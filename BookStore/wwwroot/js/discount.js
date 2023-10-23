var subtotalElements = document.querySelectorAll(".product-subtotal");
var total = 0;
for (var i = 0; i < subtotalElements.length; i++) {
    total += parseFloat(subtotalElements[i].textContent.replace('$', ''));
}
console.log(total.toFixed(2))
document.getElementById("TotalCart_SubTotal").value = total.toFixed(2);
document.getElementById("TotalCart_Total").value = total.toFixed(2);

function ApplyDiscount() {
    const discountName = document.getElementById("coupon_code").value;
    console.log(discountName)
    $.ajax({
        type: 'GET',
        url: apiUrl + '/api/Discounts/GetName/' + discountName,
        contentType: 'application/json', 
        success: function (response) {
            document.getElementById("TotalCart_DiscountId").value = response.id;  // get discount Id
            
            var discountListItem = document.getElementById("apply_discount");
            discountListItem.style.display = "block";  // display li
            document.getElementById("cart_discount").value = response.percentage;

            // Calculate and update the 'total' element (if you have the subtotal value)
            const subtotal = document.getElementById("TotalCart_SubTotal").value;
            const total = subtotal - (subtotal * (response.percentage / 100));
            console.log(total)
            document.getElementById("TotalCart_Total").value = total.toFixed(2);
        },
        error: function (xhr, status, error) {
            console.log(xhr)
            Swal.fire('Error!', 'Cannot found Discount code \"' + discountName +'\" could not be found.', 'error');
        }
    });
}