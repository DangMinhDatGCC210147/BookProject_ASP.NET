const apiUrl = localStorage.getItem("apiUrl");

$(document).ready(function () {
    $('#myForm').submit(function (e) {
        e.preventDefault();

        const orderId = $('#orderId').val();
        const orderName = $('#orderName').val();
        const orderDescription = $('#orderDescription').val();

        const data = {
            name: orderName,
            description: orderDescription,
        };

        console.log(data); // Test data

        if (orderId) {
            // Edit state
            $.ajax({
                url: apiUrl + '/api/Orders/' + orderId,
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

                    const order = document.getElementById('row_' + orderId);
                    order.innerHTML = `
                        <th scope="row">${orderId}</th>
                        <td>${response.name}</td>
                        <td>${response.description}</td>
                        <td>${formatDate(response.addDate)}</td>
                        <td>
                            ${mapApprovalStatus(response.approvalStatus)}
                        </td>
                        <td>
                            <div class="flex-column align-items-center">
                                <button type="button" class="btn btn-danger" onclick="deleteOrder(${orderId})">Delete</button>
                                <button type="button" class="btn btn-warning edit-order" data-toggle="modal" data-target="#orderModal" onclick="handleEditButton(${orderId})">Edit</button>
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
                url: apiUrl + '/api/Orders',
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

                    console.log(response);

                    const id = response.id;
                    const name = response.name;
                    const description = response.description;
                    const addDate = formatDate(response.addDate);
                    const approvalStatusText = mapApprovalStatus(response.approvalStatus);

                    const newOrder = document.createElement('tr');
                    newOrder.setAttribute('id', 'row_' + id);
                    newOrder.innerHTML = `
                        <th scope="row">${id}</th>
                        <td>${name}</td>
                        <td>${description}</td>
                        <td>${addDate}</td>
                        <td>
                            ${approvalStatusText}
                        </td>
                        <td>
                            <div class="flex-column align-items-center">
                                <button type="button" class="btn btn-danger" onclick="deleteOrder(${id})">Delete</button>
                                <button type="button" class="btn btn-warning edit-order" data-toggle="modal" data-target="#orderModal" onclick="handleEditButton(${id})">Edit</button>
                            </div>
                        </td>
                    `;

                    const table = document.getElementById('orderList');
                    const firstRow = table.getElementsByTagName('tr')[0]; // Get the first row of the table
                    table.insertBefore(newOrder, firstRow);

                    // Clear input in Popup 
                    $('#orderName').val('');
                    $('#orderDescription').val('');

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

    // X? l? hi?n th? approval status
    function mapApprovalStatus(approvalStatus) {
        switch (approvalStatus) {
            case 0:
                return "Pending";
            case 1:
                return "Accepted";
            case 2:
                return "Rejected";
            default:
                return "Unknown"; // X? l? giá tr? không h?p l? (n?u có)
        }
    }

    // Xác ð?nh s? ki?n khi ngý?i dùng nh?p vào ô t?m ki?m
    $('#searchInput').on('input', function () {
        var searchText = $(this).val().toLowerCase();
        var found = false;

        // L?p qua t?ng d?ng trong b?ng danh sách ngôn ng?
        $('#orderTable tbody tr').each(function () {
            var rowText = $(this).text().toLowerCase();

            // So sánh vãn b?n c?a t?ng d?ng v?i vãn b?n t?m ki?m
            if (rowText.includes(searchText)) {
                $(this).show();
                found = true;
            } else {
                $(this).hide();
            }
        });

        // Hi?n th? thông báo khi không có k?t qu? t?m th?y
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
function deleteOrder(id) {
    // Hi?n th? m?t h?p tho?i xác nh?n trý?c khi xóa
    Swal.fire({
        title: 'Are you sure?',
        text: 'You won\'t be able to revert this!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, cancel'
    }).then((result) => {
        if (result.isConfirmed) {
            // N?u ngý?i dùng ð?ng ? xóa, th?c hi?n AJAX ð? g?i yêu c?u xóa
            $.ajax({
                type: 'DELETE',
                url: apiUrl + '/api/Orders/' + id,
                success: function () {
                    // N?u xóa thành công, c?p nh?t giao di?n ngý?i dùng b?ng cách xóa d?ng trong b?ng
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

// Clear all input when user click on Add new button
function handleAddButton() {
    $("#orderId").val("");
    $("#orderName").val("");
    $("#orderDescription").val("");
}

// Fill data in input when user click on Edit button
function handleEditButton(id) {
    $.ajax({
        type: 'GET',
        url: apiUrl + '/api/Orders/' + id,
        success: function (response) {
            console.log(response);
            const id = response.id;
            const name = response.name;
            const description = response.description;
            $("#orderId").val(id);
            $("#orderName").val(name);
            $("#orderDescription").val(description);
        },
        error: function (xhr, status, error) {
            console.log(xhr);
            Swal.fire('Error!', 'An error occurred while deleting the record.', 'error');
        }
    });
}

function displayOrderdetail(orderid) {
    console.log(orderid)

    $.ajax({
        type: 'GET',
        url: apiUrl + '/api/OrderDetails/' + orderid,
        success: function (response) {
            console.log(response)
            var table = document.getElementById('orderDetailList');
            while (table.firstChild) {
                table.removeChild(table.firstChild)
            }

            response.forEach(od => {
                const booktitle = od.book.title
                const bookimage = od.book.image
                const quantity = od.quantity
                const unitPrice = od.unitPrice

                const newOrderDetail = document.createElement('tr');

                newOrderDetail.innerHTML = `
                    <th scope="row">${booktitle}</th>
                    <td><img src="${apiUrl}/${bookimage}" /></td>
                    <td>${quantity}</td>
                    <td>$${unitPrice}</td>
                `;
                
                const firstRow = table.getElementsByTagName('tr')[0]; // Get the first row of the table
                table.insertBefore(newOrderDetail, firstRow);
            })
        },
        error: function (xhr, status, error) {
            console.log(xhr);
            Swal.fire('Error!', 'An error occurred while deleting the record.', 'error');
        }
    });
}
