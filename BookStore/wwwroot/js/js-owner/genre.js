const apiUrl = localStorage.getItem("apiUrl")

$(document).ready(function () {
    $('#myForm').submit(function (e) {
        e.preventDefault();

        const genreId = $('#genreId').val()
        const genreName = $('#genreName').val()
        const genreDescription = $('#genreDescription').val()

        const data = {
            name: genreName,
            description: genreDescription,
        }
        console.log(data)//Test data
        if (genreId) {
            // Edit state
            $.ajax(
                {
                url: apiUrl + '/api/Genres/' + genreId,
                type: 'PUT',
                contentType: 'application/json',
                data: JSON.stringify(data),
                success: function (response) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Your work has been edited',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    console.log(response);

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
                                                <button type="button" class="btn btn-danger" onclick="deleteGenre(${genreId})">Delete</button>
                                                <button type="button" class="btn btn-warning edit-genre" data-toggle="modal" data-target="#genreModal" onclick="handleEditButton(${genre.Id})">Edit</button>
                                            </div>
                                        </td>
                    `;

                    // Close the modal
                    var closeButton = document.querySelector('.modal-footer button[data-dismiss="modal"]');
                    closeButton.click();
                },
                error: function (xhr, status, error) {
                    // Handle errors (if any)
                    console.error(xhr);
                    alert('An error occurred while sending data.');
                }
            });
        } else {
            // Add state
            $.ajax({
                url: apiUrl + '/api/Genres',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(data),
                success: function (response) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Your work has been saved',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    console.log(response);
                    const id = response.id;
                    const name = response.name;
                    const description = response.description;
                    const addDate = formatDate(response.addDate);
                    const approvalStatusText = mapApprovalStatus(response.approvalStatus);

                    const newGenre = document.createElement('tr');
                    newGenre.setAttribute('id', 'row_' + id);
                    newGenre.innerHTML = `
                                        <th scope="row">${id}</th>
                                        <td>${name}</td>
                                        <td>${description}</td>
                                        <td>${addDate}</td>
                                        <td>
                                            ${approvalStatusText}
                                        </td>
                                        <td>
                                            <div class="flex-column align-items-center">
                                                <button type="button" class="btn btn-danger" onclick="deleteGenre(${id})">Delete</button>
                                                <button type="submit" class="btn btn-warning edit-genre" data-toggle="modal" data-target="#genreModal" onclick="handleEditButton(${id})">Edit</button>
                                            </div>
                                        </td>
                    `;
                    const table = document.getElementById('genreList');
                    const firstRow = table.getElementsByTagName('tr')[0]; // Get the first row of the table
                    table.insertBefore(newGenre, firstRow);

                    // Clear input in Popup 
                    $('#genreName').val('');
                    $('#genreDescription').val('');
                    // Close the modal
                    var closeButton = document.querySelector('.modal-footer button[data-dismiss="modal"]');
                    closeButton.click();
                },
                error: function (xhr, status, error) {
                    // Handle errors (if any)
                    console.error(xhr);
                    alert('An error occurred while sending data.');
                }
            });
        }
    });
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

// === Delete ===
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
                url: apiUrl + '/api/Genres/' + id,
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

// Clear all input when user click on Add new button
function handleAddButton() {
    $("#genreId").val("");
    $("#genreName").val("");
    $("#genreDescription").val("");
}


// Fill data in input when user click on Edit button
function handleEditButton(id) {
    $.ajax({
        type: 'GET',
        url: apiUrl + '/api/Genres/' + id,
        success: function (response) {
            console.log(response)
            const id = response.id
            const name = response.name;
            const description = response.description;
            $("#genreId").val(id);
            $("#genreName").val(name);
            $("#genreDescription").val(description);
        },
        error: function (xhr, status, error) {
            console.log(xhr)
            Swal.fire('Error!', 'An error occurred while deleting the record.', 'error');
        }
    });
}
