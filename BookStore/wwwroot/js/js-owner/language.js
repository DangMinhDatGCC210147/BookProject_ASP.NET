const apiUrl = localStorage.getItem("apiUrl")

$(document).ready(function () {
    $('#myForm').submit(function (e) {
        e.preventDefault();

        const languageId = $('#languageId').val()
        const languageName = $('#languageName').val()

        const data = {
            name: languageName
        }

        if (languageId) {
            // Edit state
            $.ajax({
                url: apiUrl + '/api/Languages/' + languageId,
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

                    const language = document.getElementById('row_' + languageId);
                    language.innerHTML = `
                                        <th scope="row">${languageId}</th>
                                        <td>${response.name}</td>
                                        <td>
                                            <div class="flex-column align-items-center">
                                                <button type="button" class="btn btn-danger" onclick="deleteLanguage(${languageId})">Delete</button>
                                                <button type="submit" class="btn btn-warning edit-language" data-toggle="modal" data-target="#languageModal" onclick="handleEditButton(${languageId})">Edit</button>
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
                url: apiUrl + '/api/Languages',
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

                    const newLanguage = document.createElement('tr');
                    newLanguage.setAttribute('id', 'row_' + id);
                    newLanguage.innerHTML = `
                                        <th scope="row">${id}</th>
                                        <td>${name}</td>
                                        <td>
                                            <div class="flex-column align-items-center">
                                                <button type="button" class="btn btn-danger" onclick="deleteLanguage(${id})">Delete</button>
                                                <button type="submit" class="btn btn-warning edit-language" data-toggle="modal" data-target="#languageModal" onclick="handleEditButton(${id})">Edit</button>
                                            </div>
                                        </td>
                    `;
                    const table = document.getElementById('languageList');
                    const firstRow = table.getElementsByTagName('tr')[0]; // Get the first row of the table
                    table.insertBefore(newLanguage, firstRow);

                    // Clear input in Popup 
                    $('#languageName').val('');
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

    // Xác định sự kiện khi người dùng nhập vào ô tìm kiếm
    $('#searchInput').on('input', function () {
        var searchText = $(this).val().toLowerCase();
        var found = false;

        // Lặp qua từng dòng trong bảng danh sách ngôn ngữ
        $('#languageTable tbody tr').each(function () {
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
    
// === Delete ===
function deleteLanguage(id) {
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
                url: apiUrl + '/api/Languages/' + id,
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
    $("#languageId").val("");
    $("#languageName").val("");
}


// Fill data in input when user click on Edit button
function handleEditButton(id) {
    $.ajax({
        type: 'GET',
        url: apiUrl + '/api/Languages/' + id,
        success: function (response) {
            console.log(response)
            const id = response.id
            const name = response.name;
            $('#languageId').val(id)
            $('#languageName').val(name)
        },
        error: function (xhr, status, error) {
            console.log(xhr)
            Swal.fire('Error!', 'An error occurred while deleting the record.', 'error');
        }
    });
}
