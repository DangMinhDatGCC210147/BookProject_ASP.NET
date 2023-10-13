const apiUrl = localStorage.getItem("apiUrl");

$('#searchInput').on('input', function () {
    var searchText = $(this).val().toLowerCase();
    var found = false;

    // Lặp qua từng dòng trong bảng danh sách ngôn ngữ
    $('#reviewTable tbody tr').each(function () {
        var rowText = $(this).text().toLowerCase();

        // So sánh văn bản của từng dòng với văn bản tìm kiếm
        if (rowText.includes(searchText)) {
            $(this).show();
            found = true;
        } else {
            $(this).hide();
        }
    });

    // Hiển thị thông báo khi không có kết quả tìm thấy
    if (!found) {
        $('#noResultsMessage').show();
    } else {
        $('#noResultsMessage').hide();
    }
});
// === Delete ===
function deleteReview(id) {
    // Hiển thị một hộp thoại xác nhận trước khi xóa
    Swal.fire({
        title: 'Are you sure?',
        text: 'You won\'t be able to revert this!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, cancel'
    }).then((result) => {
        if (result.isConfirmed) {
            // Nếu người dùng đồng ý xóa, thực hiện AJAX để gửi yêu cầu xóa
            $.ajax({
                type: 'DELETE',
                url: apiUrl + '/api/Reviews/' + id,
                success: function () {
                    // Nếu xóa thành công, cập nhật giao diện người dùng bằng cách xóa dòng trong bảng
                    $('#row_' + id).remove();
                    Swal.fire('Deleted!', 'Your record has been deleted.', 'success');

                },
                error: function (xhr, status, error) {
                    console.log(xhr)
                    Swal.fire('Error!', 'An error occurred while deleting the record.', 'error');
                }
            });
        }
    });
}