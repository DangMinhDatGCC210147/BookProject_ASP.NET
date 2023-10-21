const apiUrl = localStorage.getItem("apiUrl");
//========================================================================================================
//================================== FUNCTION TO GET LIST ================================================
//========================================================================================================
//Lấy ds Language
$(document).ready(function () {
    // Gọi API để lấy danh sách ngôn ngữ và cập nhật dropdownlist
    $.ajax({
        url: apiUrl + '/api/Languages',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var dropdown = $('#languageDropdown');
            dropdown.empty();
            dropdown.append($('<option>').val('').text('Select a language'));
            $.each(data, function (key, entry) {
                dropdown.append($('<option>').val(entry.id).text(entry.name));
            });
        }
    });
});
//Lấy ds Genre
$(document).ready(function () {
    $.ajax({
        url: apiUrl + '/api/Genres',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var dropdown = $('#genreDropdown');
            dropdown.empty();
            dropdown.append($('<option>').val('').text('Select a genre'));

            // Lọc dữ liệu theo approvalStatus bằng 1 hoặc "Accepted"
            var filteredData = data.filter(function (entry) {
                return entry.approvalStatus === 1 || entry.approvalStatus === "Accepted";
            });

            $.each(filteredData, function (key, entry) {
                dropdown.append($('<option>').val(entry.id).text(entry.name));
            });
        }
    });
});

//Lấy ds Publisher
$(document).ready(function () {
    // Gọi API để lấy danh sách ngôn ngữ và cập nhật dropdownlist
    $.ajax({
        url: apiUrl + '/api/Publishers',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var dropdown = $('#publisherDropdown');
            dropdown.empty();
            dropdown.append($('<option>').val('').text('Select a publisher'));
            $.each(data, function (key, entry) {
                dropdown.append($('<option>').val(entry.id).text(entry.name));
            });
        }
    });
});
//Lấy ds Author
$(document).ready(function () {
    // Gọi API để lấy danh sách ngôn ngữ và cập nhật dropdownlist
    $.ajax({
        url: apiUrl + '/api/Authors',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var dropdown = $('#authorDropdown');
            dropdown.empty();
            dropdown.append($('<option>').val('').text('Select a author'));
            $.each(data, function (key, entry) {
                dropdown.append($('<option>').val(entry.id).text(entry.name));
            });
        }
    });
});
//========================================================================================================
//================================== FUNCTION TO CHANGE NAME =============================================
//========================================================================================================
$(".fileInput").on("change", function () {
    var fileName = $(this).val().split("\\").pop();
    $(this).siblings(".fileInputLable").addClass("selected").html(fileName);
});
//========================================================================================================
//================================== FUNCTION TO INTERACT ================================================
//========================================================================================================

$(document).ready(function () {
    $('#myForm').submit(function (e) {
        e.preventDefault();

        const bookId = $('#bookId').val();
        const bookName = $('#bookName').val();
        const genreDropdown = $('#genreDropdown').val();
        const pageCount = $('#pages').val();
        const bookDescription = $('#bookDescription').val();
        const actualPrice = $('#actualPrice').val();
        const realPrice = $('#realPrice').val();
        const quantity = $('#quantity').val();
        const isbn = $('#isbn').val();
        const datePublish = $('#datePublish').val();
        const publisherDropdown = $('#publisherDropdown').val();
        const authorDropdown = $('#authorDropdown').val();
        const fileInput = $('#fileInput')[0].files[0];
        const languageDropdown = $('#languageDropdown').val();
        const saleDropdown = $('#saleDropdown').val();
        console.log("Test: " + publisherDropdown)
        console.log("Test: " + genreDropdown)
        console.log("Test: " + authorDropdown)
        console.log("Test: " + languageDropdown)
        console.log("Test: " + saleDropdown)
        console.log("Test Image: " + fileInput)
        const data = {
            title: bookName,
            description: bookDescription,
            image: fileInput,
            quantity: quantity,
            originalPrice: actualPrice,
            sellingPrice: realPrice,  
            isbn: isbn,
            pageCount: pageCount,
            isSale: saleDropdown,
            publicationYear: datePublish,
            publisherId: publisherDropdown,
            languageId: languageDropdown,
            authorId: authorDropdown,
            genreId: genreDropdown,    
        };

        if (bookId) {
            // Edit state
            $.ajax({
                url: apiUrl + '/api/Products/' + bookId,
                type: 'PUT',
                contentType: 'application/json',
                data: JSON.stringify(data),
                success: function (response) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Your work has been edited',
                        showConfirmButton: false,
                        timer: 1500
                    });
                    console.log(response);

                    // Thay đổi dữ liệu trên giao diện khi sửa bản ghi
                    //...

                    // Đóng modal
                    var closeButton = document.querySelector('.modal-footer button[data-dismiss="modal"]');
                    closeButton.click();
                },
                error: function (xhr, status, error) {
                    console.error(xhr);
                    alert('An error occurred while sending data.');
                }
            });
        } else {
            // Add state
            $.ajax({
                url: apiUrl + '/api/Products',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(data),
                success: function (response) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Your work has been saved',
                        showConfirmButton: false,
                        timer: 1500
                    });

                    const idBook = response.id
                    const title = response.title
                    const description = response.description
                    const image = response.image
                    const quantity = response.quantity
                    const originalPrice = response.originalPrice
                    const actualPrice = response.actualPrice
                    const isbn = response.isbn
                    const pageCount = response.pageCount
                    const isSale = response.isSale
                    const publicationYear = response.publicationYear
                    const publisherId = response.publisherId
                    const languageId = response.languageId
                    const authorId = response.authorId
                    const genreId = response.genreId

                    const newBook = document.createElement('tr');
                    newBook.setAttribute('id', 'row_' + idBook);
                    newBook.innerHTML = `
                        <th scope="row">${idBook}</th>
                        <td>${title}</td>
                        <td>${description}</td>
                        <td><img scr="${image}"></img></td>
                        <td>${quantity}</td>
                        <td>${originalPrice}</td>
                        <td>${actualPrice}</td>
                        <td>${isbn}</td>
                        <td>${pageCount}</td>
                        <td>${isSale}</td>
                        <td>${formatDate(publicationYear)}</td>
                        <td>${publisherId}</td>
                        <td>${languageId}</td>
                        <td>${authorId}</td>
                        <td>${genreId}</td>
                        <td>
                            <div class="flex-column align-items-center">
                                <button type="button" class="btn btn-danger" onclick="deleteBook(${idBook})">Delete</button>
                                <button type="submit" class="btn btn-warning edit-book" data-toggle="modal" data-target="#bookModal" onclick="handleEditButton(${idBook})">Edit</button>
                            </div>
                        </td>
                    `;

                    const table = document.getElementById('bookList');
                    const firstRow = table.getElementsByTagName('tr')[0]; // Get the first row of the table
                    table.insertBefore(newBook, firstRow);

                    // Clear input in Popup 
                    $('#bookName').val('');
                    $('#genreDropdown').val('');
                    $('#bookDescription').val('');
                    $('#actualPrice').val('');
                    $('#realPrice').val('');
                    $('#quantity').val('');
                    $('#isbn').val('');
                    $('#datePublish').val('');
                    $('#publisherDropdown').val('');
                    $('#authorDropdown').val('');
                    $('#fileInput').val('');
                    $('#languageDropdown').val('');
                    $('#discountDropdown').val('');
                    $('#saleDropdown').val('');

                    // Close the modal
                    var closeButton = document.querySelector('.modal-footer button[data-dismiss="modal"]');
                    closeButton.click();
                },
                error: function (xhr, status, error) {
                    console.error(xhr);
                    alert('An error occurred while sending data.');
                }
            });
        }
    });

    $('#searchInput').on('input', function () {
        var searchText = $(this).val().toLowerCase();
        var found = false;

        $('#discountTable tbody tr').each(function () {
            var rowText = $(this).text().toLowerCase();

            if (rowText.includes(searchText)) {
                $(this).show();
                found = true;
            } else {
                $(this).hide();
            }
        });

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
function deleteDiscount(id) {
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
                url: apiUrl + '/api/Discounts/' + id,
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
    $("#discountId").val("");
    $("#discountName").val("");
    $("#discountPercentage").val("");
    $("#discountStartDate").val("");
    $("#discountEndDate").val("");
}

// Fill data in input when user click on Edit button
function handleEditButton(id) {
    $.ajax({
        type: 'GET',
        url: apiUrl + '/api/Discounts/' + id,
        success: function (response) {
            console.log(response)
            const id = response.id;
            const name = response.discountName;
            const percentage = response.percentage;
            const startdate = formatDate(response.startDate);
            const endDate = formatDate(response.endDate);
            $("#discountId").val(id);
            $("#discountName").val(name);
            $("#percentage").val(percentage);
            $("#startDate").val(startdate);
            $("#endDate").val(endDate);
        },
        error: function (xhr, status, error) {
            console.log(xhr)
            Swal.fire('Error!', 'An error occurred while deleting the record.', 'error');
        }
    });
}