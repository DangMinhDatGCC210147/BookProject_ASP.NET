const apiUrl = localStorage.getItem("apiUrl")
const userId = localStorage.getItem("userId")

SubTotal();

function SubTotal() {
    var subtotalElements = document.querySelectorAll("td.product-subtotal");
    var total = 0;
    for (var i = 0; i < subtotalElements.length; i++) {
        total += parseFloat(subtotalElements[i].textContent.replace('$', ''));
    }
    $("#subTotal").text(total.toFixed(2));

}
function DeleteCart(bookId) {
    Swal.fire({
        title: 'Are you sure?',
        text: 'Book \"' + $("#product-name").text() + '\" will be removed in your cart',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, cancel'
    }).then((result) => {
        if (result.isConfirmed) {
            const data = {
                bookId: bookId,
                newQuantity: 0,
                userId: userId
            }
            $.ajax({
                type: 'DELETE',
                url: apiUrl + '/api/CartDetails',
                data: JSON.stringify(data),
                contentType: 'application/json',
                success: function () {
                    Swal.fire('Deleted!', 'Book \"' + $("#product-name").text() + '\" was removed in your cart', 'success');
                    $('#row_' + bookId).remove();
                    SubTotal();
                },
                error: function (xhr, status, error) {
                    console.log(xhr)
                    Swal.fire('Error!', 'An error occurred while deleting the record.', 'error');
                }
            });
        }
    });
}

//Update quantity
function UpdateQuantity(action_bookId) {
    var arr = action_bookId.split('_');
    var quantity = parseInt(document.getElementById("quantity_" + arr[1]).textContent, 10);
    if (arr[0] == "minus" && quantity == 1) {
        DeleteCart(arr[1]);
        return;
    }
    arr[0] == "minus" ? quantity -= 1 : quantity += 1;

    const data = {
        bookId: arr[1],
        newQuantity: quantity,
        userId: userId
    }

    $.ajax({
        type: 'PUT',
        url: apiUrl + '/api/CartDetails',
        data: JSON.stringify(data),
        contentType: 'application/json',
        success: function () {
            $('#quantity_' + arr[1]).text(data.newQuantity);
            var quantity = parseFloat($('#quantity_' + arr[1]).text());
            var price = parseFloat($('#price_' + arr[1]).text().replace('$', ''));
            var unitTotal = (quantity * price).toFixed(2);
            console.log(unitTotal);
            $("#unitTotal_" + arr[1]).text("$" + unitTotal);
            SubTotal();
        },
        error: function (xhr, status, error) {
            console.log(xhr)
            Swal.fire('Error!', 'An error occurred while deleting the record.', 'error');
        }
 
    });
}