const apiUrl = localStorage.getItem("apiUrl")

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


// Xử lý nút "Reject"
$('.reject-button').on('click', function () {
    const genreId = $(this).data('id'); // Lấy id của genre từ thuộc tính data
    updateApprovalStatus(genreId, 2); // Gửi yêu cầu Reject
});

// Xử lý nút "Accept"
$('.accept-button').on('click', function () {
    const genreId = $(this).data('id'); // Lấy id của genre từ thuộc tính data
    updateApprovalStatus(genreId, 1); // Gửi yêu cầu Accept
});

function updateApprovalStatus(genreId, approvalStatus) {
    // Gửi yêu cầu Ajax để cập nhật trường ApprovalStatus
    $.ajax({
        url: apiUrl + '/api/Genres/' + genreId + '/approvalStatus',
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(approvalStatus),
        success: function () {
            // Xử lý thành công
            Swal.fire({
                icon: 'success',
                title: 'Your work has been done',
                showConfirmButton: false,
                timer: 1500
            })
            const genre = document.getElementById('row_' + genreId);
            genre.innerHTML = `
                                        <th scope="row">${genreId}</th>
                                        <td>${response.name}</td>
                                        <td>${response.description}</td>
                                        <td>${formatDate(response.addDate)}</td>
                                        <td>
                                            ${mapApprovalStatus(response.approvalStatus)}
                                        </td>
                                        <td>
                                            <div class="flex-column align-items-center">
                                                <button class="reject-button btn btn-danger" data-id="${genreId}">Reject</button>
                                                <button class="accept-button btn btn-success" data-id="${genreId}">Accept</button>
                                            </div>
                                        </td>
                    `;
            console.log('Trạng thái đã được cập nhật thành công.');
            // Cập nhật giao diện hoặc thực hiện các hành động khác (tuỳ theo yêu cầu)
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi nếu có
            console.error(error);
        }
    });
}


