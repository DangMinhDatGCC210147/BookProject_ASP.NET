
        // Refresh
        function refreshGenreList() {
            $.ajax({
                type: 'GET',
                url: '/api/Genres', // Địa chỉ API để tải lại danh sách thể loại
                dataType: 'json',
                success: function (data) {
                    // Cập nhật phần tử HTML để hiển thị danh sách thể loại
                    $('#genreList tbody').empty(); // Xóa dữ liệu cũ
                    $.each(data, function (index, genre) {
                        var approvalStatusText;
                        switch (genre.approvalStatus) {
                            case 0:
                                approvalStatusText = "Pending";
                                break;
                            case 1:
                                approvalStatusText = "Accepted";
                                break;
                            case 2:
                                approvalStatusText = "Rejected";
                                break;
                            default:
                                approvalStatusText = "Unknown";
                                break;
                        }
                        function formatDate(date) {
                            var year = date.getFullYear();
                            var month = (date.getMonth() + 1).toString().padStart(2, '0'); // Adding 1 to month because it's zero-based
                            var day = date.getDate().toString().padStart(2, '0');
                            return year + '-' + month + '-' + day;
                        }

                        var formattedDate = formatDate(new Date(genre.addDate)); // Format the date
                        var row = '<tr>' +
                            '<td>' + genre.id + '</td>' +
                            '<td>' + genre.name + '</td>' +
                            '<td>' + genre.description + '</td>' +
                            '<td>' + formattedDate + '</td>' +
                            '<td>' + approvalStatusText + '</td>' +
                            '<td><div class="flex-column align-items-center">' +
                            '<button type="button" class="btn btn-danger" style="margin-right: 5px;" onclick="deleteGenre(' + genre.id + ') ">Delete</button>' +
                            '<button type="button" class="btn btn-warning">Edit</button>' +
                            '</div></td>' +
                            '</tr>';
                        // Đổ dòng dữ liệu vào bảng
                        $('#genreList tbody').append(row);
                    });
                    // Đóng modal
                    $('#genreModal').modal('hide'); // Đóng modal popup
                },
                error: function (error) {
                    console.error('Error:', error);
                }
            });
        }

        function deleteGenre(id) {
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
                        url: 'api/Genres/' + id,
                        success: function () {
                            // Nếu xóa thành công, cập nhật giao diện người dùng bằng cách xóa dòng trong bảng
                            $('#genreList tbody tr[data-id="' + id + '"]').remove();
                            Swal.fire('Deleted!', 'Your record has been deleted.', 'success');

                            // Sau khi xóa thành công, gọi hàm refresh để tải lại dữ liệu mới
                            refreshGenreList();
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
            $('#genreList tbody tr').each(function () {
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