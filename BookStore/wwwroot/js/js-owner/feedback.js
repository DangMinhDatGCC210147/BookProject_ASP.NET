const apiUrl = localStorage.getItem("apiUrl");

$(document).ready(function () {
    $('#myForm').submit(function (e) {
        e.preventDefault();

        const feedbackId = $('#feedbackId').val();
        const feedbackTitle = $('#feedbackTitle').val();
        const feedbackContent = $('#feedbackContent').val();

        const data = {
            title: feedbackTitle,
            content: feedbackContent
        };

        if (feedbackId) {
            // Edit state
            $.ajax({
                url: apiUrl + '/api/Feedbacks/' + feedbackId,
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

                    const feedback = document.getElementById('row_' + feedbackId);
                    feedback.innerHTML = `
                        <th scope="row">${feedbackId}</th>
                        <td>${response.title}</td>
                        <td>${response.content}</td>
                        <td>
                            <div class="flex-column align-items-center">
                                <button type="button" class="btn btn-danger" onclick="deleteFeedback(${feedbackId})">Delete</button>
                                <button type="submit" class="btn btn-warning edit-feedback" data-toggle="modal" data-target="#feedbackModal" onclick="handleEditButton(${feedbackId})">Edit</button>
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
                url: apiUrl + '/api/Feedbacks',
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
                    const title = response.title;
                    const content = response.content;

                    const newFeedback = document.createElement('tr');
                    newFeedback.setAttribute('id', 'row_' + id);
                    newFeedback.innerHTML = `
                        <th scope="row">${id}</th>
                        <td>${title}</td>
                        <td>${content}</td>
                        <td>
                            <div class="flex-column align-items-center">
                                <button type="button" class="btn btn-danger" onclick="deleteFeedback(${id})">Delete</button>
                                <button type="submit" class="btn btn-warning edit-feedback" data-toggle="modal" data-target="#feedbackModal" onclick="handleEditButton(${id})">Edit</button>
                            </div>
                        </td>
                    `;

                    const table = document.getElementById('feedbackList');
                    const firstRow = table.getElementsByTagName('tr')[0]; // Get the first row of the table
                    table.insertBefore(newFeedback, firstRow);

                    // Clear input in Popup 
                    $('#feedbackTitle').val('');
                    $('#feedbackContent').val('');
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

    $('#searchInput').on('input', function () {
        var searchText = $(this).val().toLowerCase();
        var found = false;

        $('#feedbackTable tbody tr').each(function () {
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

function deleteFeedback(id) {
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
                url: apiUrl + '/api/Feedbacks/' + id,
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
    $("#feedbackId").val("");
    $("#feedbackTitle").val("");
    $("#feedbackContent").val("");
}

function handleEditButton(id) {
    $.ajax({
        type: 'GET',
        url: apiUrl + '/api/Feedbacks/' + id,
        success: function (response) {
            console.log(response);
            const id = response.id;
            const title = response.title;
            const content = response.content;
            $('#feedbackId').val(id);
            $('#feedbackTitle').val(title);
            $('#feedbackContent').val(content);
        },
        error: function (xhr, status, error) {
            console.log(xhr);
            Swal.fire('Error!', 'An error occurred while deleting the record.', 'error');
        }
    });
}
