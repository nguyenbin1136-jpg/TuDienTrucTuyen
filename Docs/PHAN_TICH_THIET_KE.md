# BÁO CÁO PHÂN TÍCH VÀ THIẾT KẾ WEBSITE TRA CỨU TỪ ĐIỂN TRỰC TUYẾN

## I. Khảo sát và phân tích bài toán

### 1. Xác định yêu cầu bài toán

Bài toán xây dựng website tra cứu từ điển trực tuyến bằng ASP.NET Web Forms (.NET Framework 4.8). Website cho phép tra cứu nhiều loại từ điển, xem nghĩa, phiên âm, từ loại và ví dụ sử dụng của từ, tra chéo giữa các từ ngay trên website, và chuyển từ khóa sang website từ điển bên ngoài để đối chiếu.

Trong phạm vi học phần Lập trình Web Cơ bản, chương trình không sử dụng SQL Server. Dữ liệu được lưu trong hai tệp XML đặt tại `App_Data` (`words.xml`, `users.xml`) thông qua lớp `DataStore` trong `App_Code`. Các tệp XML được khởi tạo tự động lần đầu chạy nếu chưa tồn tại, bao gồm 6 từ mẫu và một tài khoản quản trị viên mặc định.

### 2. Đối tượng và người sử dụng

**Nhóm 1 – Quản trị viên (`role = admin`):** đăng nhập vào khu vực quản trị; thêm, sửa, xóa từ vựng; xem danh sách tài khoản; xem và cập nhật thông tin cá nhân; đổi mật khẩu.

**Nhóm 2 – Thành viên đã đăng nhập (`role = user`):** tra cứu từ vựng; xem chi tiết từ; tra cứu trên website ngoài; xem lịch sử tra cứu trong phiên; xem và cập nhật trang cá nhân; đổi mật khẩu.

**Nhóm 3 – Khách (chưa đăng nhập):** tra cứu từ vựng; xem chi tiết từ; tra cứu trên website ngoài. Không có lịch sử tra cứu, không truy cập được trang cá nhân và quản trị.

---

## II. Phân tích chức năng nghiệp vụ

### 1. Bảng chức năng theo nhóm người dùng

| Chức năng | Quản trị viên | Thành viên | Khách |
|---|:---:|:---:|:---:|
| Tra cứu từ điển (nội bộ) | ✓ | ✓ | ✓ |
| Xem chi tiết từ vựng | ✓ | ✓ | ✓ |
| Tra cứu website bên ngoài | ✓ | ✓ | ✓ |
| Đăng ký tài khoản | — | — | ✓ |
| Đăng nhập / Đăng xuất | ✓ | ✓ | — |
| Lịch sử tra cứu (phiên) | ✓ | ✓ | — |
| Trang cá nhân (xem/sửa) | ✓ | ✓ | — |
| Đổi mật khẩu | ✓ | ✓ | — |
| Thêm / sửa / xóa từ vựng | ✓ | — | — |
| Xem danh sách người dùng | ✓ | — | — |
| Bảng điều khiển quản trị | ✓ | — | — |

### 2. Mô tả nhóm chức năng

- **Tra cứu từ điển:** nhập từ khóa, chọn loại từ điển (Tất cả / Anh–Việt / Việt–Anh / Anh–Anh), hiển thị danh sách kết quả xếp theo độ phù hợp. Có thể tra nhanh từ trang chủ hoặc tra nâng cao tại trang TraCuu.
- **Xem chi tiết từ:** hiển thị đầy đủ phiên âm, từ loại, nghĩa, ví dụ, từ đồng nghĩa và các liên kết tra chéo tới Cambridge, Oxford và Google.
- **Tra cứu chéo ngoài:** chuyển từ khóa tới Cambridge Dictionary, Oxford Learner's Dictionaries, Google Translate hoặc Google Search.
- **Tài khoản:** đăng ký (tạo tài khoản nhóm user), đăng nhập (xác thực PBKDF2), đăng xuất (xóa Session), cập nhật họ tên, đổi mật khẩu.
- **Lịch sử tra cứu:** lưu tối đa 30 lượt tìm kiếm gần nhất vào Session trong phiên đăng nhập hiện tại. Hiển thị tối đa 5 lượt gần nhất trên trang cá nhân và toàn bộ trên trang lịch sử.
- **Quản trị dữ liệu:** thêm mới, chỉnh sửa và xóa từ vựng qua giao diện GridView; xem danh sách tài khoản; xem thống kê số từ và số tài khoản trên bảng điều khiển.

### 3. Sơ đồ chức năng dạng cây

```
WEBSITE TRA CỨU TỪ ĐIỂN TRỰC TUYẾN
│
├── 1. Tra cứu từ điển
│   ├── Tìm kiếm nhanh (trang chủ)
│   ├── Tra cứu nâng cao (TraCuu.aspx)
│   │   ├── Lọc theo loại từ điển
│   │   └── Hiển thị danh sách kết quả
│   ├── Xem chi tiết từ (ChiTietTu.aspx)
│   │   ├── Nghĩa, phiên âm, từ loại, ví dụ, từ đồng nghĩa
│   │   └── Liên kết tra chéo ngoài
│   └── Tra cứu website bên ngoài (TraCuuNgoai.aspx)
│       ├── Cambridge Dictionary
│       ├── Oxford Learner's Dictionaries
│       ├── Google Translate
│       └── Google Search
│
├── 2. Tài khoản
│   ├── Đăng ký (Register.aspx)
│   ├── Đăng nhập (Login.aspx)
│   ├── Đăng xuất (Logout.aspx)
│   ├── Trang cá nhân (CaNhan.aspx)
│   │   ├── Cập nhật họ tên hiển thị
│   │   ├── Đổi mật khẩu
│   │   └── Xem 5 lượt tra cứu gần nhất
│   └── Lịch sử tra cứu (LichSu.aspx)
│
└── 3. Quản trị (Admin – chỉ quản trị viên)
    ├── Bảng điều khiển (Admin/Default.aspx)
    │   └── Thống kê số từ, số tài khoản
    ├── Quản lý từ vựng (Admin/QuanLyTu.aspx)
    │   ├── Thêm từ mới
    │   ├── Sửa từ hiện có
    │   └── Xóa từ
    └── Quản lý người dùng (Admin/QuanLyNguoiDung.aspx)
        └── Xem danh sách tài khoản và vai trò
```

---

## III. Thiết kế hệ thống

### 1. Kiến trúc tổng thể

```
Trình duyệt
    │  HTTP Request/Response
    ▼
Site.Master  ←── layout chung (navbar, footer, link CSS/JS)
    │
    ├── Default.aspx          Trang chủ
    ├── TraCuu.aspx           Tra cứu nâng cao
    ├── ChiTietTu.aspx        Chi tiết từ
    ├── TraCuuNgoai.aspx      Tra cứu ngoài
    ├── Login / Register /    Tài khoản
    │   Logout / CaNhan /
    │   LichSu
    └── Admin/                Khu vực quản trị
        ├── Default.aspx
        ├── QuanLyTu.aspx
        └── QuanLyNguoiDung.aspx

App_Code/
    ├── DictionaryModels.cs   Lớp DictionaryWord, DictionaryUser
    └── DataStore.cs          Đọc/ghi XML, tìm kiếm, xác thực

App_Data/
    ├── words.xml             Dữ liệu từ vựng
    └── users.xml             Dữ liệu tài khoản (không truy cập qua URL)
```

### 2. Tiến trình Input – Process – Output

**Tra cứu từ (TraCuu.aspx):**
- Input: từ khóa (chuỗi), loại từ điển.
- Process: chuẩn hóa từ khóa (chuyển thường, bỏ dấu tiếng Việt); duyệt danh sách XML; lọc theo loại; tính điểm phù hợp trên các trường Word, Meaning, Example, Synonyms; sắp xếp giảm dần theo điểm.
- Output: danh sách `DictionaryWord` theo thứ tự độ phù hợp, hoặc thông báo không tìm thấy.

**Xem chi tiết từ (ChiTietTu.aspx):**
- Input: tham số `id` trên query string.
- Process: tìm mục từ theo Id trong XML.
- Output: trang hiển thị đầy đủ thông tin từ và liên kết tra chéo ngoài.

**Tra cứu ngoài (TraCuuNgoai.aspx):**
- Input: từ khóa, nguồn tra cứu được chọn.
- Process: mã hóa từ khóa theo URL; tạo địa chỉ tương ứng nguồn đã chọn.
- Output: chuyển hướng (Redirect) trình duyệt sang website nguồn.

**Đăng nhập (Login.aspx):**
- Input: tên đăng nhập, mật khẩu.
- Process: tìm tài khoản trong `users.xml`; kiểm tra mật khẩu bằng PBKDF2 (20 000 vòng lặp, muối ngẫu nhiên 16 byte); so sánh hằng thời gian (constant-time).
- Output: tạo Session (`Username`, `DisplayName`, `Role`) và chuyển hướng; hoặc thông báo lỗi.

**Thêm/sửa/xóa từ (Admin/QuanLyTu.aspx):**
- Input: dữ liệu từ vựng (loại, từ, phiên âm, từ loại, nghĩa, ví dụ, từ đồng nghĩa), thao tác.
- Process: kiểm tra Session `Role = admin`; kiểm tra trường bắt buộc (từ, nghĩa); đọc/ghi `words.xml` với khóa (lock) tránh xung đột đồng thời.
- Output: thông báo kết quả, cập nhật GridView.

**Cập nhật trang cá nhân (CaNhan.aspx):**
- Input: họ tên mới hoặc mật khẩu hiện tại + mật khẩu mới + xác nhận mật khẩu.
- Process: kiểm tra Session đăng nhập; kiểm tra dữ liệu hợp lệ; xác minh mật khẩu hiện tại bằng PBKDF2; cập nhật `users.xml`.
- Output: thông báo thành công/thất bại; Session `DisplayName` được đồng bộ sau khi đổi tên.

### 3. Thiết kế dữ liệu (lớp trong App_Code)

**Lớp `DictionaryWord`** (`App_Code/DictionaryModels.cs`):

| Thuộc tính | Kiểu | Mô tả |
|---|---|---|
| `Id` | string | Mã định danh, duy nhất (số nguyên hoặc GUID) |
| `DictionaryType` | string | Loại từ điển: `Anh - Việt`, `Việt - Anh`, `Anh - Anh` |
| `Word` | string | Từ hoặc cụm từ |
| `Pronunciation` | string | Phiên âm IPA |
| `PartOfSpeech` | string | Từ loại (Danh từ, Động từ, Tính từ…) |
| `Meaning` | string | Nghĩa hoặc định nghĩa |
| `Example` | string | Câu ví dụ |
| `Synonyms` | string | Từ đồng nghĩa / từ liên quan |
| `Score` | int | Điểm độ phù hợp khi tìm kiếm (không lưu XML) |

**Lớp `DictionaryUser`** (`App_Code/DictionaryModels.cs`):

| Thuộc tính | Kiểu | Mô tả |
|---|---|---|
| `Username` | string | Tên đăng nhập (không phân biệt hoa/thường) |
| `DisplayName` | string | Họ tên hiển thị |
| `PasswordHash` | string | `Base64(salt):Base64(hash)` — PBKDF2-SHA1, 20 000 vòng |
| `Role` | string | `admin` hoặc `user` |

**Tệp lưu trữ:**

| Tệp | Vị trí | Mô tả |
|---|---|---|
| `words.xml` | `App_Data/` | Danh sách từ vựng; mỗi phần tử `<Word>` có thuộc tính `id`, `type` và các phần tử con tương ứng |
| `users.xml` | `App_Data/` | Danh sách tài khoản; mỗi phần tử `<User>` lưu `username`, `name`, `role`, `password` |

Thư mục `App_Data` bị khóa truy cập qua URL theo cấu hình trong `Web.config` (`<deny users="*"/>`).

**Lịch sử tra cứu:** lưu dạng `List<string>` vào `Session["SearchHistory"]`, tối đa 30 mục, định dạng `"từ khóa · dd/MM/yyyy HH:mm"`. Dữ liệu mất khi phiên kết thúc hoặc người dùng đăng xuất.

### 4. Thuật toán tìm kiếm (DataStore.GetWords)

Thuật toán thực hiện qua hai bước:

**Bước 1 – Chuẩn hóa (Normalize):**
Chuyển từ khóa và nội dung từng trường về dạng chữ thường, không dấu tiếng Việt (Unicode NFD + loại bỏ diacritics, đổi `đ` → `d`). Nhờ đó người dùng gõ `chao` vẫn tìm thấy `chào`, gõ `dep` vẫn tìm thấy `đẹp`.

**Bước 2 – Chấm điểm (MatchScore):**

| Trường hợp khớp | Điểm |
|---|---|
| Từ trùng khớp chính xác | 1000 |
| Từ bắt đầu bằng từ khóa (prefix) | 500 |
| Từ chứa từ khóa | 200 |
| Nghĩa (Meaning) chứa từ khóa | 60 |
| Từ đồng nghĩa (Synonyms) chứa từ khóa | 40 |
| Ví dụ (Example) chứa từ khóa | 30 |

Kết quả được sắp xếp giảm dần theo tổng điểm. Mục từ có tổng điểm = 0 bị loại khỏi danh sách. Khi không có từ khóa (tra theo loại từ điển), bỏ qua bước chấm điểm, trả về toàn bộ theo thứ tự trong XML.

### 5. Bảo mật

- **Mật khẩu:** băm bằng PBKDF2 (Rfc2898DeriveBytes, SHA-1, 20 000 vòng lặp, muối ngẫu nhiên 16 byte, hash 32 byte). Mỗi lần đăng ký tạo muối mới, không lưu mật khẩu gốc.
- **Phân quyền:** các trang quản trị kiểm tra `Session["Role"] == "admin"` ở đầu `Page_Load`; người không đủ quyền bị chuyển về trang đăng nhập.
- **Trang cá nhân:** yêu cầu đăng nhập; đổi mật khẩu phải nhập đúng mật khẩu hiện tại trước.
- **Chống XSS:** toàn bộ dữ liệu hiển thị dùng `<%#: Eval(...) %>` (HTML-encode tự động) hoặc `Server.HtmlEncode`.
- **App_Data:** bị khóa truy cập trực tiếp qua HTTP theo cấu hình `<location path="App_Data">` trong `Web.config`.
- **Cookie phiên:** `httpOnlyCookies="true"` — không thể truy cập từ JavaScript.
- **Đầu vào:** dữ liệu người dùng nhập được trim và kiểm tra độ dài trước khi lưu; query string cho tra chéo ngoài được URL-encode.

### 6. Cấu hình hệ thống (Web.config)

| Mục | Giá trị | Ghi chú |
|---|---|---|
| Target Framework | .NET 4.8 | `<compilation targetFramework="4.8">` |
| Encoding | UTF-8 | Toàn bộ request/response/file |
| Culture | vi-VN | Định dạng ngày tháng |
| Session timeout | 30 phút | InProc |
| httpOnlyCookies | true | Bảo vệ cookie phiên |
| customErrors | RemoteOnly | Lỗi runtime chuyển tới `Error.aspx` |

---

## IV. Thiết kế giao diện

### 1. Bố cục chung (Site.Master)

Tất cả 12 trang đều dùng `Site.Master` làm layout chung. File CSS duy nhất được liên kết tại `Site.Master` bằng `ResolveUrl("~/Content/style.css")`, đảm bảo đường dẫn đúng kể cả với các trang trong thư mục con `/Admin/`.

Bố cục gồm 3 phần:
- **Header (`.topbar`):** logo, menu điều hướng chính, khu vực tài khoản (nút Đăng nhập/Đăng ký khi chưa đăng nhập; tên người dùng, nút Tài khoản và Đăng xuất khi đã đăng nhập).
- **Main (`.container.page`):** `ContentPlaceHolder` nhận nội dung riêng từng trang.
- **Footer (`.footer`):** tên website và bản quyền.

### 2. Danh sách trang và chức năng

| Tệp | Tiêu đề | Chức năng chính |
|---|---|---|
| `Default.aspx` | Trang chủ | Tìm kiếm nhanh, giới thiệu loại từ điển, hiển thị từ vựng nổi bật |
| `TraCuu.aspx` | Tra cứu từ điển | Tìm kiếm nâng cao, lọc loại từ điển, danh sách kết quả |
| `ChiTietTu.aspx` | Chi tiết từ vựng | Hiển thị đầy đủ thông tin từ, liên kết tra chéo |
| `TraCuuNgoai.aspx` | Tra cứu ngoài | Chọn nguồn, chuyển từ khóa sang website ngoài |
| `Login.aspx` | Đăng nhập | Form đăng nhập, chuyển hướng sau đăng nhập |
| `Register.aspx` | Đăng ký | Form tạo tài khoản mới |
| `Logout.aspx` | Đăng xuất | Xóa Session, chuyển về trang chủ |
| `CaNhan.aspx` | Trang cá nhân | Cập nhật họ tên, đổi mật khẩu, xem lịch sử gần đây |
| `LichSu.aspx` | Lịch sử tra cứu | Danh sách toàn bộ từ khóa đã tra trong phiên |
| `Error.aspx` | Lỗi | Thông báo lỗi runtime |
| `Admin/Default.aspx` | Bảng điều khiển | Thống kê từ vựng, tài khoản; điều hướng quản trị |
| `Admin/QuanLyTu.aspx` | Quản lý từ vựng | Form thêm/sửa từ, GridView danh sách từ |
| `Admin/QuanLyNguoiDung.aspx` | Quản lý người dùng | GridView danh sách tài khoản và vai trò |

### 3. Các thành phần CSS và JS

**File:** `Content/style.css` — stylesheet duy nhất, không dùng framework ngoài.

Các nhóm class chính:

| Nhóm | Class | Dùng tại |
|---|---|---|
| Navbar | `.topbar`, `.nav`, `.brand`, `.main-menu`, `.account` | Site.Master |
| Hero | `.hero`, `.hero-inner`, `.eyebrow` | Default.aspx |
| Search box | `.searchbox` | Default.aspx |
| Lưới thẻ | `.grid`, `.grid-2`, `.tile`, `.tile-icon` | Default.aspx, Admin |
| Thẻ từ vựng | `.word-card`, `.word-top`, `.pron`, `.tag`, `.meaning`, `.example` | TraCuu, ChiTietTu |
| Khung nội dung | `.panel` | TraCuu, Admin, CaNhan, Login… |
| Form | `.form-row`, `.field`, `.input`, `.select`, `.textarea` | TraCuu, Admin, CaNhan… |
| Nút | `.btn`, `.btn-primary`, `.btn-outline`, `.btn-small` | Toàn site |
| Thông báo | `.note`, `.message`, `.message.success`, `.message.error` | Nhiều trang |
| Tiêu đề | `.page-title`, `.sub-title`, `.section-head` | Nhiều trang |
| Admin | `.stats`, `.stat`, `.admin-nav` | Các trang Admin |
| Bảng | `table`, `th`, `td` | GridView, LichSu |
| Trang cá nhân | `.profile-header`, `.profile-avatar`, `.profile-grid`, `.profile-history` | CaNhan |
| Tiện ích | `.muted`, `.empty`, `.eyebrow`, `.text-link` | Nhiều trang |

**File:** `Scripts/app.js` — JavaScript tối thiểu, không dùng thư viện ngoài.

| Hàm | Mục đích |
|---|---|
| `validateSearch()` | Kiểm tra ô nhập từ khóa không để trống trước khi submit |
| `confirmDelete(message)` | Hiển thị hộp thoại xác nhận trước khi xóa từ |

Ô tìm kiếm dùng `ClientIDMode="Static"` để `validateSearch()` tìm đúng phần tử qua `getElementById("txtKeyword")`. Các form tìm kiếm bọc trong `<asp:Panel DefaultButton="...">` để Enter trên bàn phím kích hoạt đúng nút.

---

## V. Phân công công việc nhóm (mẫu cho 3 sinh viên)

| Thành viên | Công việc |
|---|---|
| Sinh viên 1 | Phân tích yêu cầu, thiết kế giao diện (`Site.Master`, `style.css`), trang chủ (`Default.aspx`), trang tra cứu (`TraCuu.aspx`, `ChiTietTu.aspx`) |
| Sinh viên 2 | Thiết kế lớp dữ liệu (`DictionaryModels.cs`, `DataStore.cs`), đọc/ghi XML, thuật toán tìm kiếm, tra cứu ngoài (`TraCuuNgoai.aspx`) |
| Sinh viên 3 | Tài khoản (`Login`, `Register`, `Logout`, `CaNhan`, `LichSu`), phân quyền, khu vực quản trị (`Admin/*`), kiểm thử và tích hợp |

Nếu nhóm 2 sinh viên: gộp Sinh viên 1 + 2 (giao diện và dữ liệu), Sinh viên 3 (tài khoản, quản trị, kiểm thử).

---

## VI. Kiểm thử chức năng

| STT | Trường hợp kiểm thử | Đầu vào | Kết quả mong đợi |
|---|---|---|---|
| 1 | Tra từ chính xác | `hello`, Anh–Việt | Hiển thị nghĩa, phiên âm, ví dụ; điểm 1000 xếp đầu |
| 2 | Tra từ không dấu | `chao`, Tất cả | Tìm thấy `xin chào` (bỏ dấu, 200+ điểm) |
| 3 | Tra theo tiền tố | `beau`, Tất cả | Tìm thấy `beautiful` (điểm 500) |
| 4 | Tra không tìm thấy | `zxqwerty`, Tất cả | Thông báo không có kết quả |
| 5 | Tra để trống từ khóa | (trống), bấm Tra cứu | Alert yêu cầu nhập từ, không gửi form |
| 6 | Enter trong ô tìm kiếm | Nhập từ, bấm Enter | Chuyển sang trang TraCuu với kết quả |
| 7 | Lọc theo loại từ điển | Chọn Việt–Anh, không nhập từ | Hiển thị toàn bộ từ loại Việt–Anh |
| 8 | Xem chi tiết từ | Bấm "Xem chi tiết" | Trang ChiTietTu hiển thị đủ thông tin và liên kết tra chéo |
| 9 | Tra cứu ngoài | `computer`, Cambridge | Chuyển hướng tới Cambridge, URL chứa `computer` |
| 10 | Đăng ký tài khoản mới | Username hợp lệ, mật khẩu ≥ 6 ký tự | Tạo tài khoản, thông báo thành công |
| 11 | Đăng ký trùng username | Username đã tồn tại | Thông báo lỗi, không tạo thêm |
| 12 | Đăng nhập đúng | admin / Admin@123 | Vào khu vực quản trị, Session có Role=admin |
| 13 | Đăng nhập sai mật khẩu | admin / sai | Thông báo lỗi, không tạo Session |
| 14 | User vào trang Admin | URL `/Admin/Default.aspx` | Chuyển về trang đăng nhập |
| 15 | Khách vào trang cá nhân | URL `/CaNhan.aspx` | Chuyển về trang đăng nhập |
| 16 | Admin thêm từ mới | Điền đủ từ và nghĩa | Từ xuất hiện trong GridView và `words.xml` |
| 17 | Admin sửa từ | Bấm Sửa, đổi nghĩa, Lưu | Dữ liệu cập nhật đúng trong XML |
| 18 | Admin xóa từ | Bấm Xóa, xác nhận | Từ bị xóa khỏi GridView và XML |
| 19 | Đổi mật khẩu đúng | Mật khẩu hiện tại đúng, mật khẩu mới ≥ 6 ký tự | Thông báo thành công, đăng nhập lại bằng mật khẩu mới |
| 20 | Lịch sử tra cứu | Tra 3 từ rồi vào LichSu | Hiển thị đúng 3 lượt với thời gian |

---

## VII. Tài liệu tham khảo

1. Tài liệu đề bài "Xây dựng website Tra cứu từ điển trực tuyến" do giảng viên cung cấp.
2. Tài liệu "Hướng dẫn bài tập lớn" do giảng viên cung cấp.
3. Microsoft Learn – ASP.NET Web Forms (.NET Framework): `https://learn.microsoft.com/aspnet/web-forms`
4. Microsoft Learn – Rfc2898DeriveBytes (PBKDF2): `https://learn.microsoft.com/dotnet/api/system.security.cryptography.rfc2898derivebytes`
5. Cambridge Dictionary: `https://dictionary.cambridge.org`
6. Oxford Learner's Dictionaries: `https://www.oxfordlearnersdictionaries.com`
7. Google Translate: `https://translate.google.com`
