// Show Cart
$(window).on("load", function () {
	const apiUrl = localStorage.getItem("apiUrl")
	$.ajax({
		url: apiUrl + "/api/CartDetails/" + userId,
		type: "GET",
		contentType: 'application/json',
		success: function (response) {
			var table = document.getElementById("listCart");
			table.innerHTML =
				`
				<div class="table-content table-responsive">
						<table>
							<thead>
								<tr>
									<th>remove</th>
									<th>images</th>
									<th>Product</th>
									<th>Price</th>
									<th>Quantity</th>
									<th>Total</th>
								</tr>
							</thead>
							<tbody id="table_row"> </tbody>
						</table>
					</div>
					<div class="row">
						<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
							<div class="coupon-all">
								<div class="coupon">
									<input id="coupon_code" class="input-text" name="coupon_code" value="" placeholder="Coupon code" type="text">
									<input class="button" name="apply_coupon" value="Apply coupon" type="submit">
								</div>
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col-md-5 ms-auto">
							<div class="cart-page-total">
								<h2>Cart totals</h2>
								<ul>
									<li>Subtotal<span id="cart_subtotal">0.00</span></li>
									<li>Total<span id="total">0.00</span></li>
								</ul>
								<a href="#">Proceed to checkout</a>
							</div>
						</div>
					</div>
			`
			var row = "";
			response.forEach(item => {
				row += `
							<tr id="row_${item.bookId}">
								<td class="product-remove"><a onclick="DeleteCart(${item.bookId})"><i class="pe-7s-close"></i></a></td>
								<td class="product-thumbnail">
									<a href="/Home/Detail/${item.bookId}"><img src="${apiUrl}/${item.image}" alt=""></a>
								</td>
								<td class="product-name"><a href="/Home/Detail/${item.bookId}" id="product-name_${item.bookId}">${item.title}</a></td>
								<td class="product-price-cart"><span class="amount" id="price_${item.bookId}">$${item.price}</span></td>
								<td class="product-quantity">
                                <div class="input-quantity">
                                      <div class="input-group-append">
                                          <div class="cart-plus-minus">
                                            <div class="inc qtybutton" onclick="UpdateQuantity('minus_${item.bookId}')">-</div>
                                          </div>                                          
                                      </div>
                                      <span id="quantity_${item.bookId}" class="form-control text-center input_quantity_box">${item.quantity}</span>
                                      <div class="input-group-append">  
                                          <div class="cart-plus-minus">
                                            <div class="inc qtybutton" onclick="UpdateQuantity('plus_${item.bookId}')">+</div>
                                          </div> 
                                      </div>
                                </div>
								</td>
								<td class="product-subtotal" id="unitTotal_${item.bookId}">$${item.subTotal}</td>
							</tr>
						`
			});
			document.getElementById("table_row").innerHTML = row;
			SubTotal();
		},
		error: function (error) {
			console.log(error)
		},
	});
});

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
            $("#unitTotal_" + arr[1]).text("$" + unitTotal);
			SubTotal();
            DisplayCartAndWishlist();
        },
        error: function (xhr, status, error) {
            console.log(xhr)
            Swal.fire('Error!', 'An error occurred while deleting the record.', 'error');
        }
 
    });
}

function SubTotal() {
	var subtotalElements = document.querySelectorAll(".product-subtotal");
	var total = 0;
	for (var i = 0; i < subtotalElements.length; i++) {
		total += parseFloat(subtotalElements[i].textContent.replace('$', ''));
	}
	$("#cart_subtotal").text(total.toFixed(2));
}
