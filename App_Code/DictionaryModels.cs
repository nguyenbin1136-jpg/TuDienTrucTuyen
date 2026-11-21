using System;
[Serializable]
public class DictionaryWord
{
    public string Id
    {
        get;
        set;
    }
    public string DictionaryType
    {
        get;
        set;
    }
    public string Word
    {
        get;
        set;
    }
    public string Pronunciation
    {
        get;
        set;
    }
    public string PartOfSpeech
    {
        get;
        set;
    }
    public string Meaning
    {
        get;
        set;
    }
    public string Example
    {
        get;
        set;
    }
    public string Synonyms
    {
        get;
        set;
    }
    // Diem uu tien khi sap xep ket qua tim kiem (khong luu vao XML).
    public int Score
    {
        get;
        set;
    }
}
[Serializable]
public class DictionaryUser
{
    public string Username
    {
        get;
        set;
    }
    public string DisplayName
    {
        get;
        set;
    }
    public string PasswordHash
    {
        get;
        set;
    }
    public string Role
    {
        get;
        set;
    }
}
