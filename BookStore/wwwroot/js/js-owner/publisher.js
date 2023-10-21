const apiUrl = localStorage.getItem("apiUrl");

$(document).ready(function () {
    $('#myForm').submit(function (e) {
        e.preventDefault();

        const publisherId = $('#publisherId').val();
        const publisherName = $('#publisherName').val();

        const data = {
            name: publisherName
        };
        console.log(data)//Test
        if (publisherId) {
            // Edit state
            $.ajax({
                url: apiUrl + '/api/Publishers/' + publisherId,
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

                    const publisher = document.getElementById('row_' + publisherId);
                    publisher.innerHTML = `
                        <th scope="row">${publisherId}</th>
                        <td>${response.name}</td>
                        <td>
                            <div class="flex-column align-items-center">
                                <button type="button" class="btn btn-danger" onclick="deletePublisher(${publisherId})">Delete</button>
                                <button type="submit" class="btn btn-warning edit-publisher" data-toggle="modal" data-target="#publisherModal" onclick="handleEditButton(${publisherId})">Edit</button>
                            </div>
                        </td>
                    `;

                    // Close the modal
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
                url: apiUrl + '/api/Publishers',
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
                    console.log(response)
                    const id = response.id;
                    const name = response.name;

                    const newPublisher = document.createElement('tr');
                    newPublisher.setAttribute('id', 'row_' + id);
                    newPublisher.innerHTML = `
                        <th scope="row">${id}</th>
                        <td>${name}</td>
                        <td>
                            <div class="flex-column align-items-center">
                                <button type="button" class="btn btn-danger" onclick="deletePublisher(${id})">Delete</button>
                                <button type="submit" class="btn btn-warning edit-publisher" data-toggle="modal" data-target="#publisherModal" onclick="handleEditButton(${id})">Edit</button>
                            </div>
                        </td>
                    `;

                    const table = document.getElementById('publisherList');
                    const firstRow = table.getElementsByTagName('tr')[0]; // Get the first row of the table
                    table.insertBefore(newPublisher, firstRow);

                    // Clear input in Popup 
                    $('#publisherName').val('');

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

        $('#publisherTable tbody tr').each(function () {
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

function deletePublisher(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: 'You won\'t be able to revert this!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, cancel'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'DELETE',
                url: apiUrl + '/api/Publishers/' + id,
                success: function () {
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

function handleAddButton() {
    $("#publisherId").val("");
    $("#publisherName").val("");
}

function handleEditButton(id) {
    $.ajax({
        type: 'GET',
        url: apiUrl + '/api/Publishers/' + id,
        success: function (response) {
            console.log(response);
            const id = response.id;
            const name = response.name;
            $('#publisherId').val(id);
            $('#publisherName').val(name);
        },
        error: function (xhr, status, error) {
            console.log(xhr);
            Swal.fire('Error!', 'An error occurred while deleting the record.', 'error');
        }
    });
}
