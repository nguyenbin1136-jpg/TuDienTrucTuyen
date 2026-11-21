using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;
public static class DataStore
{
    private static readonly object Sync = new object();
    private static string DataPath(string file)
    {
        return HttpContext.Current.Server.MapPath("~/App_Data/" + file);
    }
    public static void EnsureFiles()
    {
        lock (Sync)
        {
            string words = DataPath("words.xml");
            string users = DataPath("users.xml");
            if (!File.Exists(words))
            {
                XmlDocument d = new XmlDocument();
                d.AppendChild(d.CreateElement("Words"));
                AddWord(d, "1", "Anh - Việt", "hello", "/həˈləʊ/", "Thán từ", "Xin chào; lời chào dùng khi gặp ai đó.", "Hello, how are you? – Xin chào, bạn khỏe không?", "hi; greetings");
                AddWord(d, "2", "Anh - Việt", "book", "/bʊk/", "Danh từ", "Quyển sách; tài liệu gồm nhiều trang được đóng lại.", "I read a book every week. – Tôi đọc một quyển sách mỗi tuần.", "volume");
                AddWord(d, "3", "Anh - Việt", "computer", "/kəmˈpjuːtə(r)/", "Danh từ", "Máy tính; thiết bị điện tử dùng để xử lý và lưu trữ dữ liệu.", "My computer is new. – Máy tính của tôi mới.", "PC; machine");
                AddWord(d, "4", "Anh - Việt", "beautiful", "/ˈbjuːtɪfl/", "Tính từ", "Đẹp; có vẻ ngoài hoặc đặc điểm làm người khác thích thú.", "It is a beautiful day. – Hôm nay là một ngày đẹp trời.", "pretty; lovely");
                AddWord(d, "5", "Việt - Anh", "xin chào", "", "Thán từ", "hello; hi; greetings.", "Xin chào, tôi tên là Nam. – Hello, my name is Nam.", "chào hỏi");
                AddWord(d, "6", "Anh - Anh", "success", "/səkˈses/", "Noun", "The achievement of something that you have been trying to do.", "Hard work is the key to success.", "achievement; accomplishment");
                d.Save(words);
            }
            if (!File.Exists(users))
            {
                XmlDocument d = new XmlDocument();
                d.AppendChild(d.CreateElement("Users"));
                XmlElement e = d.CreateElement("User");
                e.SetAttribute("username", "admin");
                e.SetAttribute("name", "Quản trị viên");
                e.SetAttribute("role", "admin");
                e.SetAttribute("password", Hash("Admin@123"));
                d.DocumentElement.AppendChild(e);
                d.Save(users);
            }
        }
    }
    private static void AddWord(XmlDocument d, string id, string type, string word, string pron, string pos, string meaning, string example, string synonyms)
    {
        XmlElement e = d.CreateElement("Word");
        e.SetAttribute("id", id);
        e.SetAttribute("type", type);
        Set(e, "Text", word);
        Set(e, "Pronunciation", pron);
        Set(e, "PartOfSpeech", pos);
        Set(e, "Meaning", meaning);
        Set(e, "Example", example);
        Set(e, "Synonyms", synonyms);
        d.DocumentElement.AppendChild(e);
    }
    private static void Set(XmlElement p, string name, string value)
    {
        XmlElement e = p.OwnerDocument.CreateElement(name);
        e.InnerText = value ?? "";
        p.AppendChild(e);
    }
    private static string Val(XmlNode n, string name)
    {
        XmlNode x = n.SelectSingleNode(name);
        return x == null ? "" : x.InnerText;
    }

    // Tim tu dien theo loai va tu khoa.
    // - Loc theo loai neu chi dinh (khac "Tat ca").
    // - Neu co tu khoa: so khop khong phan biet hoa thuong va bo dau tieng Viet,
    //   tim tren nhieu truong (tu, nghia, vi du, tu dong nghia) va sap xep theo do uu tien.
    public static List<DictionaryWord> GetWords(string type, string keyword)
    {
        EnsureFiles();
        string kw = (keyword ?? "").Trim();
        string kwNorm = Normalize(kw);
        bool hasKeyword = kwNorm.Length > 0;
        bool filterType = !String.IsNullOrEmpty(type) && type != "Tất cả";

        List<DictionaryWord> list = new List<DictionaryWord>();
        lock (Sync)
        {
            XmlDocument d = new XmlDocument();
            d.Load(DataPath("words.xml"));
            foreach (XmlNode n in d.SelectNodes("/Words/Word"))
            {
                string t = n.Attributes["type"].Value;
                if (filterType && t != type) continue;

                string word = Val(n, "Text");
                string meaning = Val(n, "Meaning");
                string example = Val(n, "Example");
                string synonyms = Val(n, "Synonyms");
                string pos = Val(n, "PartOfSpeech");
                string pron = Val(n, "Pronunciation");

                if (hasKeyword)
                {
                    int score = MatchScore(kwNorm, word, meaning, example, synonyms);
                    if (score == 0) continue;
                    list.Add(new DictionaryWord
                    {
                        Id = n.Attributes["id"].Value,
                        DictionaryType = t,
                        Word = word,
                        Pronunciation = pron,
                        PartOfSpeech = pos,
                        Meaning = meaning,
                        Example = example,
                        Synonyms = synonyms,
                        Score = score
                    });
                }
                else
                {
                    list.Add(new DictionaryWord
                    {
                        Id = n.Attributes["id"].Value,
                        DictionaryType = t,
                        Word = word,
                        Pronunciation = pron,
                        PartOfSpeech = pos,
                        Meaning = meaning,
                        Example = example,
                        Synonyms = synonyms
                    });
                }
            }
        }
        if (hasKeyword) list.Sort((a, b) => b.Score.CompareTo(a.Score));
        return list;
    }

    // Tinh diem uu tien: trung khop > tien to > chua; cot "tu" trong nghia nhat.
    private static int MatchScore(string kwNorm, string word, string meaning, string example, string synonyms)
    {
        int score = 0;
        string wNorm = Normalize(word);
        if (wNorm.Length > 0)
        {
            if (wNorm == kwNorm) score += 1000;
            else if (wNorm.StartsWith(kwNorm, StringComparison.Ordinal)) score += 500;
            else if (wNorm.IndexOf(kwNorm, StringComparison.Ordinal) >= 0) score += 200;
        }
        if (Normalize(meaning).IndexOf(kwNorm, StringComparison.Ordinal) >= 0) score += 60;
        if (Normalize(synonyms).IndexOf(kwNorm, StringComparison.Ordinal) >= 0) score += 40;
        if (Normalize(example).IndexOf(kwNorm, StringComparison.Ordinal) >= 0) score += 30;
        return score;
    }

    // Chuyen ve chu thuong va bo dau tieng Viet de tim kiem khoang dung.
    private static string Normalize(string s)
    {
        if (String.IsNullOrEmpty(s)) return "";
        string v = s.ToLowerInvariant();
        if (v.IndexOf('đ') >= 0) v = v.Replace('đ', 'd');
        StringBuilder sb = new StringBuilder(v.Length);
        foreach (char c in v.Normalize(NormalizationForm.FormD))
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }
        return sb.ToString().Trim();
    }

    public static DictionaryWord GetWord(string id)
    {
        foreach (DictionaryWord w in GetWords("Tất cả", "")) if (w.Id == id) return w;
        return null;
    }
    public static void SaveWord(DictionaryWord w)
    {
        EnsureFiles();
        lock (Sync)
        {
            XmlDocument d = new XmlDocument();
            d.Load(DataPath("words.xml"));
            XmlNode old = d.SelectSingleNode("/Words/Word[@id='" + SafeXPath(w.Id) + "']");
            if (old != null) d.DocumentElement.RemoveChild(old);
            string id = String.IsNullOrEmpty(w.Id) ? Guid.NewGuid().ToString("N") : w.Id;
            XmlElement e = d.CreateElement("Word");
            e.SetAttribute("id", id);
            e.SetAttribute("type", w.DictionaryType);
            Set(e,"Text",w.Word);
            Set(e,"Pronunciation",w.Pronunciation);
            Set(e,"PartOfSpeech",w.PartOfSpeech);
            Set(e,"Meaning",w.Meaning);
            Set(e,"Example",w.Example);
            Set(e,"Synonyms",w.Synonyms);
            d.DocumentElement.AppendChild(e);
            d.Save(DataPath("words.xml"));
        }
    }
    private static string SafeXPath(string s)
    {
        return (s ?? "").Replace("'", "");
    }
    public static void DeleteWord(string id)
    {
        EnsureFiles();
        lock (Sync)
        {
            XmlDocument d=new XmlDocument();
            d.Load(DataPath("words.xml"));
            XmlNode n=d.SelectSingleNode("/Words/Word[@id='"+SafeXPath(id)+"']");
            if(n!=null)
            {
                d.DocumentElement.RemoveChild(n);
                d.Save(DataPath("words.xml"));
            }
        }
    }
    public static List<DictionaryUser> GetUsers()
    {
        EnsureFiles();
        List<DictionaryUser> list=new List<DictionaryUser>();
        lock(Sync)
        {
            XmlDocument d=new XmlDocument();
            d.Load(DataPath("users.xml"));
            foreach(XmlNode n in d.SelectNodes("/Users/User"))list.Add(new DictionaryUser
            {
                Username=n.Attributes["username"].Value,DisplayName=n.Attributes["name"].Value,Role=n.Attributes["role"].Value,PasswordHash=n.Attributes["password"].Value
            }
            );
        }
        return list;
    }
    public static DictionaryUser Login(string username,string password)
    {
        foreach(DictionaryUser u in GetUsers()) if(String.Equals(u.Username,username,StringComparison.OrdinalIgnoreCase) && Verify(password,u.PasswordHash))return u;
        return null;
    }
    public static bool Register(string username,string name,string password)
    {
        EnsureFiles();
        lock(Sync)
        {
            XmlDocument d=new XmlDocument();
            d.Load(DataPath("users.xml"));
            foreach(XmlNode n in d.SelectNodes("/Users/User"))if(String.Equals(n.Attributes["username"].Value,username,StringComparison.OrdinalIgnoreCase))return false;
            XmlElement e=d.CreateElement("User");
            e.SetAttribute("username",username);
            e.SetAttribute("name",name);
            e.SetAttribute("role","user");
            e.SetAttribute("password",Hash(password));
            d.DocumentElement.AppendChild(e);
            d.Save(DataPath("users.xml"));
            return true;
        }
    }
    public static DictionaryUser GetUser(string username)
    {
        if (String.IsNullOrWhiteSpace(username)) return null;
        foreach (DictionaryUser u in GetUsers())
            if (String.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase))
                return u;
        return null;
    }

    public static bool UpdateDisplayName(string username, string displayName)
    {
        if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(displayName)) return false;
        EnsureFiles();
        lock (Sync)
        {
            XmlDocument d = new XmlDocument();
            d.Load(DataPath("users.xml"));
            XmlNode n = d.SelectSingleNode("/Users/User[@username='" + SafeXPath(username) + "']");
            if (n == null) return false;
            n.Attributes["name"].Value = displayName.Trim();
            d.Save(DataPath("users.xml"));
            return true;
        }
    }

    public static bool ChangePassword(string username, string currentPassword, string newPassword)
    {
        if (String.IsNullOrWhiteSpace(username) || String.IsNullOrEmpty(newPassword)) return false;
        EnsureFiles();
        lock (Sync)
        {
            XmlDocument d = new XmlDocument();
            d.Load(DataPath("users.xml"));
            XmlNode n = d.SelectSingleNode("/Users/User[@username='" + SafeXPath(username) + "']");
            if (n == null || !Verify(currentPassword, n.Attributes["password"].Value)) return false;
            n.Attributes["password"].Value = Hash(newPassword);
            d.Save(DataPath("users.xml"));
            return true;
        }
    }

    public static string Hash(string value)
    {
        byte[] salt = new byte[16];
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }
        using (Rfc2898DeriveBytes derive = new Rfc2898DeriveBytes(value ?? "", salt, 20000))
        {
            byte[] hash = derive.GetBytes(32);
            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
        }
    }
    private static bool Verify(string value, string stored)
    {
        if (String.IsNullOrEmpty(stored) || !stored.Contains(":")) return false;
        string[] parts = stored.Split(':');
        try
        {
            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] expected = Convert.FromBase64String(parts[1]);
            using (Rfc2898DeriveBytes derive = new Rfc2898DeriveBytes(value ?? "", salt, 20000))
            {
                byte[] actual = derive.GetBytes(expected.Length);
                int diff = 0;
                for (int i = 0; i < expected.Length; i++) diff |= expected[i] ^ actual[i];
                return diff == 0;
            }
        }
        catch
        {
            return false;
        }
    }
}
