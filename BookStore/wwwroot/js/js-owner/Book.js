const apiUrl = localStorage.getItem("apiUrl");
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
//Delete
// === Delete ===
function deleteBook(id) {
    // Show a confirmation dialog before deletion
    Swal.fire({
        title: 'Are you sure?',
        text: 'You won\'t be able to revert this!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, cancel'
    }).then((result) => {
        if (result.isConfirmed) {
            // If the user agrees to delete, perform an AJAX request to send the delete request
            $.ajax({
                type: 'DELETE',
                url: apiUrl + '/api/Products/' + id,
                success: function () {
                    // If deletion is successful, update the user interface by removing the row from the table
                    $('#row_' + id).remove();
                    Swal.fire('Deleted!', 'Your record has been deleted.', 'success');
                },
                error: function (xhr, status, error) {
                    console.log(xhr);
                    Swal.fire('Error!', 'An error occurred while deleting the record.', 'error');
                }
            });
        }
    });
}

//================ADD AND UPDATE FUNCTION======================
$(document).ready(function () {
    $('#myForm').submit(function (e) {
        e.preventDefault();

        const bookId = $('#bookId').val();
        const bookName = $('#bookName').val();
        const genreDropdown = $('#genreDropdown').val();
        const bookDescription = $('#bookDescription').val();
        const actualPrice = $('#actualPrice').val();
        const realPrice = $('#realPrice').val();
        const quantity = $('#quantity').val();
        const isbn = $('#isbn').val();
        const datePublished = $('#datePublished').val();
        const publisherDropdown = $('#publisherDropdown').val();
        const pages = $('#pages').val();
        const fileInput = document.getElementById('fileInput');
        const selectedFile = fileInput.files[0];
        const languageDropdown = $('#languageDropdown').val();
        const authorDropdown = $('#authorDropdown').val();
        const isSale = $('#saleDropdown').val();


        const formData = new FormData();
        //formData.append('id', bookId);
        formData.append('title', bookName);
        formData.append('description', bookDescription);
        formData.append('imageFile', selectedFile);
        formData.append('quantity', quantity);
        formData.append('originalPrice', actualPrice);
        formData.append('sellingPrice', realPrice);
        formData.append('isbn', isbn);
        formData.append('publicationYear', datePublished);
        formData.append('publisherId', publisherDropdown);
        formData.append('languageId', languageDropdown);
        formData.append('authorId', authorDropdown);
        formData.append('pageCount', pages);
        formData.append('genreId', genreDropdown);
        formData.append('isSale', isSale);

        if (selectedFile) {
            formData.append('imageFile', selectedFile);
        }

        console.log("Form data:");
        formData.forEach((value, key) => {
            console.log(key + ": " + value);
        });//Test data
        if (bookId) {
            // Edit state

            $.ajax(
                {
                    url: apiUrl + '/api/Products/' + bookId,
                    type: 'PUT',
                    processData: false,
                    contentType: false,
                    data: formData,
                    success: function (response) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Your work has been edited',
                            showConfirmButton: false,
                            timer: 1500
                        })
                        console.log(response);
                        setTimeout(function () {
                            location.reload();
                        }, 1500);
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
                    url: apiUrl + '/api/Products',
                    type: 'POST',
                    processData: false,
                    contentType: false,
                    data: formData,
                    success: function (response) {
                        console.log("Data in reponse: ");
                        console.log(response);
                        Swal.fire({
                            icon: 'success',
                            title: 'Your work has been saved',
                            showConfirmButton: false,
                            timer: 1500
                        })
                        setTimeout(function () {
                            location.reload();
                        }, 1500);

                    },
                    error: function (xhr, status, error) {
                        // Handle errors (if any)
                        console.error(xhr);
                        alert('An error occurred while sending data.');
                    }
                });
        }
    });
});
function getPictures() {
    // Sử dụng Ajax để lấy danh sách titles
    $.ajax({
        url: apiUrl + '/api/Products/GetAllTitles',
        type: "GET",
        dataType: "json",
        success: function (titles) {
            titles.forEach(function (title) {
                // Sử dụng Ajax để lấy hình ảnh cho từng title
                $.ajax({
                    url: apiUrl + '/api/Products/GetImage/' + title,
                    type: "GET",
                    dataType: "json",
                    success: function (data) {
                        const base64Image = data.base64Image;
                        var cell = document.getElementById('imageContainer_' + title);
                        const imageElement = document.createElement('img');
                        imageElement.src = 'data:image/png;base64,' + base64Image;
                        cell.appendChild(imageElement);
                    },
                    error: function () {
                        console.error("Lỗi khi tải hình ảnh.");
                    }
                });
            });
        },
        error: function () {
            console.error("Lỗi khi tải danh sách titles.");
        }
    });
}


// Gọi API để lấy danh sách các Title
getPictures();

//Search
$(document).ready(function () {
    // Xác định sự kiện khi người dùng nhập vào ô tìm kiếm
    $('#searchInput').on('input', function () {
        var searchText = $(this).val().toLowerCase();
        var found = false;

        // Lặp qua từng dòng trong bảng danh sách book
        $('#bookTable tbody tr').each(function () {
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

// Fill data in input when user click on Edit button
function handleEditButton(id) {
    $.ajax({
        type: 'GET',
        url: apiUrl + '/api/Products/' + id,
        success: function (response) {
            console.log(response)
            const id = response.id;
            const title = response.title;
            const description = response.description;
            const originalPrice = response.originalPrice;
            const sellingPrice = response.sellingPrice;
            const isSale = response.isSale;
            const quantity = response.quantity;
            const isbn = response.isbn;
            const publicationYear = response.publicationYear;
            const publisherId = response.publisherId;
            const pageCount = response.pageCount;
            const languageId = response.languageId;
            const genreId = response.genreId;
            const authorId = response.authorId;
            const fileName = response.title

            $('#bookId').val(id);
            $('#bookName').val(title);
            $('#genreDropdown').val(genreId);
            $('#bookDescription').val(description);
            $('#actualPrice').val(originalPrice);
            $('#realPrice').val(sellingPrice);
            $('#quantity').val(quantity);
            $('#isbn').val(isbn);
            $('#datePublished').val(publicationYear);
            $('#publisherDropdown').val(publisherId);
            $('#pages').val(pageCount);
            $('#languageDropdown').val(languageId);
            $('#authorDropdown').val(authorId);
            
            if (isSale) {
                document.getElementById('saleDropdown').value = 'true'; // Đặt cho giá trị tương ứng khi là true
            } else {
                document.getElementById('saleDropdown').value = 'false'; // Đặt cho giá trị tương ứng khi là false
            }
            const imageFileName = response.title; // Lấy tên tệp hình ảnh từ response
            const imageSrc = apiUrl + '/GetImage?imagePath=' + imageFileName;
            $('#fileInput').val(imageSrc);

        },
        error: function (xhr, status, error) {
            console.log(xhr)
            Swal.fire('Error!', 'An error occurred while deleting the record.', 'error');
        }
    });
}

//Xử lý lable cho file input
function updateLabel(input) {
    const selectedFile = input.files[0];
    if (selectedFile) {
        // Cập nhật nội dung của label với tên của tệp
        const label = input.nextElementSibling; // Lấy đối tượng label
        label.textContent = selectedFile.name;
    } else {
        // Nếu không có tệp nào được chọn, reset nội dung của label
        const label = input.nextElementSibling; // Lấy đối tượng label
        label.textContent = 'Choose an image:';
    }
}

