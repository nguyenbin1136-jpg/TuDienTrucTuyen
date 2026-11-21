# WEBSITE TRA CỨU TỪ ĐIỂN TRỰC TUYẾN

## 1. Phạm vi bài tập
- ASP.NET Web Forms, C# và HTML/CSS/JavaScript.
- Không sử dụng SQL Server, không cần tạo database.
- Dữ liệu từ vựng và tài khoản được lưu bằng XML trong `App_Data`.
- Mô hình dữ liệu được biểu diễn bằng các lớp `DictionaryWord`, `DictionaryUser` trong `App_Code` theo hướng dẫn bài tập lớn.

## 2. Yêu cầu môi trường
- Windows 10/11.
- Visual Studio 2019/2022 có workload ASP.NET and web development.
- .NET Framework 4.5 Developer Pack hoặc cao hơn.
- IIS Express (được cài cùng Visual Studio).

## 3. Cách mở và chạy
1. Giải nén `TuDienTrucTuyen_ASPNET_NoSQL.zip`.
2. Mở Visual Studio.
3. Chọn `File > Open > Web Site...`.
4. Chọn thư mục `TuDienTrucTuyen` vừa giải nén.
5. Trong Solution Explorer, chọn `Default.aspx`, nhấn chuột phải và chọn `Set as Start Page` nếu cần.
6. Nhấn `Ctrl + F5` để chạy bằng IIS Express.
7. Lần chạy đầu, chương trình tự tạo `App_Data/words.xml` và `App_Data/users.xml`.

Không chạy bằng Live Server của VS Code vì đây là ứng dụng ASP.NET Web Forms cần máy chủ ASP.NET.

## 4. Tài khoản quản trị mẫu
- Tên đăng nhập: `admin`
- Mật khẩu: `Admin@123`
- Sau khi đăng nhập, mở mục Quản trị để quản lý từ vựng và xem tài khoản.

## 5. Chức năng
- Tra cứu từ điển Anh - Việt, Việt - Anh, Anh - Anh.
- Xem nghĩa, phiên âm, từ loại, ví dụ và từ đồng nghĩa.
- Tra chéo từ điển nội bộ.
- Tra cứu từ bên ngoài thông qua Cambridge, Oxford, Google Translate và Google Search.
- Đăng ký, đăng nhập, phân quyền quản trị viên/thành viên.
- Quản trị viên thêm, sửa, xóa từ vựng; xem danh sách tài khoản.
- Thành viên có trang cá nhân: xem tên đăng nhập/vai trò, cập nhật họ tên hiển thị, đổi mật khẩu bằng cách xác nhận mật khẩu hiện tại, xem nhanh 5 lượt tra cứu gần nhất.
- Thành viên xem lịch sử tra cứu trong phiên đăng nhập.

## 6. Trang cá nhân người dùng
- Đăng nhập và chọn **Trang cá nhân** trên thanh điều hướng hoặc mục **Tài khoản**.
- Có thể cập nhật họ tên hiển thị; tên đăng nhập và vai trò chỉ đọc.
- Có thể đổi mật khẩu nếu nhập đúng mật khẩu hiện tại; mật khẩu mới tối thiểu 6 ký tự và được băm trước khi lưu.
- Hoạt động gần đây hiển thị tối đa 5 lượt tra cứu trong phiên hiện tại.

## 7. Vị trí lưu dữ liệu
- `App_Data/words.xml`: dữ liệu từ vựng.
- `App_Data/users.xml`: tài khoản, vai trò và mật khẩu đã băm PBKDF2 với salt riêng.

Sao lưu hai tệp này khi chuyển máy. Không xóa thư mục `App_Data` khi đang sử dụng.

## 8. Lưu ý bảo mật và triển khai
- `App_Data` được ASP.NET chặn truy cập trực tiếp từ trình duyệt.
- Mật khẩu không lưu dạng văn bản thuần.
- Nội dung từ vựng được mã hóa HTML khi hiển thị.
- Trang quản trị kiểm tra vai trò trong Session.
- Bản demo dùng Session InProc và file XML, phù hợp phạm vi học phần không SQL; nếu triển khai Internet thực tế cần bổ sung HTTPS, CSRF token, giới hạn đăng nhập và lưu trữ dữ liệu có khóa đồng bộ phù hợp.
