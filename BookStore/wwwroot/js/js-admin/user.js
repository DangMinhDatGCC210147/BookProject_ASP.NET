const apiUrl = localStorage.getItem("apiUrl")

$(document).ready(function () {
    $('#myForm').submit(function (e) {
        e.preventDefault();

        const data = {
            firstName: $("#firstName").val(),
            lastName: $("#lastName").val(),
            address: $("#address").val(),
            phoneNumber: $("#phoneNumber").val(),
            email: $("#email").val(),
            password: $("#password").val()
        };

        if (userId) {
            // Edit state
            $.ajax({
                url: apiUrl + '/api/Users/' + userId,
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

                    //const language = document.getElementById('row_' + languageId);
                    //language.innerHTML = `
                    //                    <th scope="row">${languageId}</th>
                    //                    <td>${response.name}</td>
                    //                    <td>
                    //                        <div class="flex-column align-items-center">
                    //                            <button type="button" class="btn btn-danger" onclick="deleteLanguage(${languageId})">Delete</button>
                    //                            <button type="submit" class="btn btn-warning edit-language" data-toggle="modal" data-target="#languageModal" onclick="handleEditButton(${languageId})">Edit</button>
                    //                        </div>
                    //                    </td>
                    //`;

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
                url: apiUrl + '/api/Users',
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
                    //const id = response.id;
                    //const name = response.name;

                    //const newLanguage = document.createElement('tr');
                    //newLanguage.setAttribute('id', 'row_' + id);
                    //newLanguage.innerHTML = `
                    //                    <th scope="row">${id}</th>
                    //                    <td>${name}</td>
                    //                    <td>
                    //                        <div class="flex-column align-items-center">
                    //                            <button type="button" class="btn btn-danger" onclick="deleteLanguage(${id})">Delete</button>
                    //                            <button type="submit" class="btn btn-warning edit-language" data-toggle="modal" data-target="#languageModal" onclick="handleEditButton(${id})">Edit</button>
                    //                        </div>
                    //                    </td>
                    //`;
                    //const table = document.getElementById('languageList');
                    //const firstRow = table.getElementsByTagName('tr')[0]; // Get the first row of the table
                    //table.insertBefore(newLanguage, firstRow);

                    //// Clear input in Popup 
                    //$('#languageName').val('');
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