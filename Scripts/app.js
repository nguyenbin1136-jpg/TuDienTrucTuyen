function confirmDelete(message) {
    return window.confirm(message || 'Bạn có chắc chắn muốn xóa dữ liệu này?');
}

// Kiem tra o tim kiem khong de trong truoc khi gui.
// Dung id tinh "txtKeyword" (ClientIDMode=Static) de tim dung o nhap.
function validateSearch(inputId) {
    var input = document.getElementById(inputId || 'txtKeyword');
    if (input && input.value.trim() === '') {
        alert('Vui lòng nhập từ cần tra cứu.');
        input.focus();
        return false;
    }
    return true;
}
