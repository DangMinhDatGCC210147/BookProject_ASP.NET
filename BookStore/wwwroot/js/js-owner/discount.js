const apiUrl = localStorage.getItem("apiUrl");

$(document).ready(function () {
    $('#myForm').submit(function (e) {
        e.preventDefault();

        const discountId = $('#discountId').val();
        const discountName = $('#discountName').val();
        const discountPercentage = $('#percentage').val();
        const discountStartDate = $('#startDate').val();
        const discountEndDate = $('#endDate').val();

        const data = {
            discountName: discountName,
            percentage: discountPercentage,
            startDate: discountStartDate,
            endDate: discountEndDate,
        };
        
        if (discountId) {
            // Edit state
            $.ajax({
                url: apiUrl + '/api/Discounts/' + discountId,
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
                    console.log(response)
                    const discount = document.getElementById('row_' + discountId);
                    discount.innerHTML = `
                        <th scope="row">${response.id}</th>
                        <td>${response.discountName}</td>
                        <td>${response.percentage}</td>
                        <td>${formatDate(response.startDate)}</td>
                        <td>${formatDate(response.endDate)}</td>
                        <td>
                            <div class="flex-column align-items-center">
                                <button type="button" class="btn btn-danger" onclick="deleteDiscount(${discountId})">Delete</button>
                                <button type="submit" class="btn btn-warning edit-discount" data-toggle="modal" data-target="#discountModal" onclick="handleEditButton(${response.id})">Edit</button>
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
            console.log(data)
            // Add state
            $.ajax({
                url: apiUrl + '/api/Discounts',
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

                    const id = response.id;
                    const name = response.discountName;
                    const percentage = response.percentage;
                    const startDate = response.startDate;
                    const endDate = response.endDate;

                    const newDiscount = document.createElement('tr');
                    newDiscount.setAttribute('id', 'row_' + id);
                    newDiscount.innerHTML = `
                        <th scope="row">${id}</th>
                        <td>${name}</td>
                        <td>${percentage}</td>
                        <td>${formatDate(startDate)}</td>
                        <td>${formatDate(endDate)}</td>
                        <td>
                            <div class="flex-column align-items-center">
                                <button type="button" class="btn btn-danger" onclick="deleteDiscount(${id})">Delete</button>
                                <button type="submit" class="btn btn-warning edit-discount" data-toggle="modal" data-target="#discountModal" onclick="handleEditButton(${id})">Edit</button>
                            </div>
                        </td>
                    `;

                    const table = document.getElementById('discountList');
                    const firstRow = table.getElementsByTagName('tr')[0]; // Get the first row of the table
                    table.insertBefore(newDiscount, firstRow);

                    // Clear input in Popup 
                    $('#discountName').val('');
                    $('#percentage').val('');
                    $('#startDate').val('');
                    $('#endDate').val('');

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
        $('#discountTable tbody tr').each(function () {
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
