

// Hàm để làm mới dữ liệu bảng
    function refreshLanguageList() {
        $.ajax({
            type: 'GET',
            url: 'https://localhost:7269/api/Languages', // Địa chỉ API để tải lại danh sách ngôn ngữ
            dataType: 'json',
            success: function (data) {
                // Cập nhật phần tử HTML để hiển thị danh sách ngôn ngữ
                $('#languageList tbody').empty(); // Xóa dữ liệu cũ
                $.each(data, function (index, language) {
                    var row = '<tr>' +
                        '<td>' + language.id + '</td>' +
                        '<td>' + language.name + '</td>' +
                        '<td><div class="flex-column align-items-center">' +
                        '<button type="button" class="btn btn-danger" style="margin-right: 5px;" onclick="deleteLanguage(' + language.id + ') ">Delete</button>' +
                        '<button type="button" class="btn btn-warning edit-language">Edit</button>' +
                        '</div></td>' +
                        '</tr>';
                    // Đổ dòng dữ liệu vào bảng
                    $('#languageList tbody').append(row);
                });
            },
            error: function (error) {
                console.error('Error:', error);
            }
        });
    }
    //Add
    $(document).ready(function () {
        $('#myForm').submit(function (e) {
            e.preventDefault();

            // Get the anti-forgery token value from the hidden input field
            var antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();
            // Get the value from the "Name" input field
            var Name = $('#Name').val();
            // Create a JSON data object to send to the API
            var data = {
                Name: Name
            };
            // Include the anti-forgery token in the request headers
            // Use AJAX to send data to the API
            $.ajax({
                type: 'POST',
                url: 'https://localhost:7269/api/Languages',
                data: JSON.stringify(data),
                contentType: 'application/json',
                success: function (response) {
                    // Clear input
                    $('#Name').val('');
                    // Đóng modal
                    $('#languageModal').modal('hide');
                    Swal.fire({
                        icon: 'success',
                        title: 'Your work has been saved',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    refreshLanguageList();
                },
                error: function (error) {
                    // Handle errors (if any)
                    console.error('Error:', error);
                    alert('An error occurred while sending data.');
                }
            });
        });
    });

    // === Delete ===
function deleteLanguage(id) {
    console.log(id)
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
                url: 'https://localhost:7269/api/Languages/' + id,
                success: function () {
                    // Nếu xóa thành công, cập nhật giao diện người dùng bằng cách xóa dòng trong bảng

                    var removeRow = document.getElementById("row-" + id);
                    removeRow.remove();
                    Swal.fire('Deleted!', 'Your record has been deleted.', 'success');

                    // Sau khi xóa thành công, gọi hàm refresh để tải lại dữ liệu mới
                    refreshLanguageList();
                },
                error: function () {
                    Swal.fire('Error!', 'An error occurred while deleting the record.', 'error');
                }
            });
        }
    });
}
   


    $(document).ready(function () {
        // Xác định sự kiện khi người dùng nhập vào ô tìm kiếm
        $('#searchInput').on('input', function () {
            var searchText = $(this).val().toLowerCase();
            var found = false;

            // Lặp qua từng dòng trong bảng danh sách ngôn ngữ
            $('#languageList tbody tr').each(function () {
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
    });
