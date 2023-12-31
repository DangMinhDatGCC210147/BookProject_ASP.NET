﻿/*const apiUrl = localStorage.getItem("apiUrl")*/

    //Xử lý hiển thị approval status
    function mapApprovalStatus(approvalStatus) {
        switch (approvalStatus) {
            case 0:
                return "Pending";
            case 1:
                return "Accepted";
            case 2:
                return "Rejected";
            default:
                return "Unknown"; // Xử lý giá trị không hợp lệ (nếu có)
        }
    }
    // Xác định sự kiện khi người dùng nhập vào ô tìm kiếm
    $('#searchInput').on('input', function () {
        var searchText = $(this).val().toLowerCase();
        var found = false;

        // Lặp qua từng dòng trong bảng danh sách ngôn ngữ
        $('#genreTable tbody tr').each(function () {
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

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('-');
}

// Clear all input when user click on Add new button
function handleAddButton() {
    $("#genreId").val("");
    $("#genreName").val("");
    $("#genreDescription").val("");
}
$(document).ready(function () {
    $('.reject-button').on('click', function () {
        const genreId = $(this).data('id'); 
        updateApprovalStatus(genreId, 2); 
    });

    $('.accept-button').on('click', function () {
        const genreId = $(this).data('id');
        updateApprovalStatus(genreId, 1); 
    });

    function updateApprovalStatus(genreId, approvalStatus) {
        // Gửi yêu cầu Ajax để cập nhật trường ApprovalStatus
        $.ajax({
            url: apiUrl + '/api/Genres/' + genreId + '/approvalStatus',
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(approvalStatus),
            success: function (response) {
                // Xử lý thành công
                Swal.fire({
                    icon: 'success',
                    title: 'Your work has been done',
                    showConfirmButton: false,
                    timer: 1500
                })
                const genre = document.getElementById('row_' + genreId);
                genre.querySelector('td.description').textContent = mapApprovalStatus(approvalStatus);
                console.log('Trạng thái đã được cập nhật thành công.');
            },
            error: function (xhr, status, error) {
                // Xử lý lỗi nếu có
                console.error(error);
            }
        });
    }
});





