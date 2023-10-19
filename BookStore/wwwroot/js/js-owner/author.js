const apiUrl = localStorage.getItem("apiUrl");


$(document).ready(function () {
    $('#myForm').submit(function (e) {
        e.preventDefault();

        const authorId = $('#authorId').val()
        const authorName = $('#authorName').val()
        const authorDescription = $('#authorDescription').val()
        const data = {
            name: authorName,
            description: authorDescription,
        }
        console.log(data)//Test
        if (authorId) {
            // Edit state
            $.ajax({
                url: apiUrl + '/api/Authors/' + authorId,
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

                    const author = document.getElementById('row_' + authorId);
                    author.innerHTML = `
                                        <th scope="row">${authorId}</th>
                                        <td>${authorName}</td>
                                        <td>${authorDescription}</td>
                                        <td>
                                            <div class="flex-column align-items-center">
                                                <button type="button" class="btn btn-danger" onclick="deleteAuthor(${authorId})">Delete</button>
                                                <button type="submit" class="btn btn-warning edit-author" data-toggle="modal" data-target="#authorModal" onclick="handleEditButton(${author.id})">Edit</button>
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
            console.log(data)
            $.ajax({
                url: apiUrl + '/api/Authors',
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
                    const id = response.id
                    const name = response.name
                    const description = response.description

                    const newAuthor = document.createElement('tr');
                    newAuthor.setAttribute('id', 'row_' + id);
                    newAuthor.innerHTML = `
                                        <th scope="row">${id}</th>
                                        <td>${name}</td>
                                        <td>${description}</td>
                                        <td>
                                            <div class="flex-column align-items-center">
                                               
                                                <button type="button" class="btn btn-danger" onclick="deleteAuthor(${id})">Delete</button>
                                                <button type="submit" class="btn btn-warning edit-author" data-toggle="modal" data-target="#authorModal" onclick="handleEditButton(${id})">Edit</button>
                                            </div>
                                        </td>
                    `;
                    const table = document.getElementById('authorList');
                    const firstRow = table.getElementsByTagName('tr')[0]; // Get the first row of the table
                    table.insertBefore(newAuthor, firstRow);

                    // Clear input in Popup 
                    $('#authorName').val('');
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

    // Search event when user types into the search input
    $('#searchInput').on('input', function () {
        var searchText = $(this).val().toLowerCase();
        var found = false;

        // Loop through each row in the authors table
        $('#authorTable tbody tr').each(function () {
            var rowText = $(this).text().toLowerCase();

            // Compare text of each row with the search text
            if (rowText.includes(searchText)) {
                $(this).show();
                found = true;
            } else {
                $(this).hide();
            }
        });

        // Display message when no results are found
        if (!found) {
            $('#noResultsMessage').show();
        } else {
            $('#noResultsMessage').hide();
        }
    });
});

// === Delete ===
function deleteAuthor(id) {
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
                url: apiUrl + '/api/Authors/' + id,
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

// Clear all input when user clicks on Add new button
function handleAddButton() {
    $("#authorId").val("");
    $("#authorName").val("");
    $("#authorDescription").val("");
}

// Fill data in input when user clicks on Edit button
function handleEditButton(id) {
    $.ajax({
        type: 'GET',
        url: apiUrl + '/api/Authors/' + id,
        success: function (response) {
            //console.log(response)
            const id = response.id
            const name = response.name;
            const description = response.description;
            $('#authorId').val(id);
            $('#authorName').val(name);
            $('#authorDescription').val(description);
        },
        error: function (xhr, status, error) {
            console.log(xhr);
            Swal.fire('Error!', 'An error occurred while deleting the record.', 'error');
        }   
    });
}
