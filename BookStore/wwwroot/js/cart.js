$(window).on("load", function () {
	$.ajax({
		url: apiUrl + "/api/CartDetails/" + userId,
		type: "GET",
		contentType: 'application/json',
		success: function (response) {
			console.log(response);
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
									<li>Subtotal<span id="subTotal">0.00</span></li>
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
									<a href="#"><img src="/img/product/book/${item.image}" alt=""></a>
								</td>
								<td class="product-name"><a href="#" id="product-name">${item.image}</a></td>
								<td class="product-price-cart"><span class="amount" id="price_${item.bookId}">$${item.price}</span></td>
								<td class="product-quantity">
									<div class="cart-plus-minus">
										<div class="inc qtybutton" onclick="UpdateQuantity('plus_${item.bookId}')">+</div>
										<div class="inc qtybutton" onclick="UpdateQuantity('minus_${item.bookId}')">-</div>
										<span id="quantity_${item.bookId}">${item.quantity}</span>
									</div>
								</td>
								<td class="product-subtotal" id="unitTotal_${item.bookId}">$${item.subTotal}</td>
							</tr>
						`
			});
			document.getElementById("table_row").innerHTML = row;

		},
		error: function (error) {
			console.log(error)
		},
	});
});

function AddToCart(bookId) {
    if (userId) {
        $.ajax({
            url: apiUrl + "/api/Carts/?userId=" + userId,
            type: "POST",
            success: function (response) {
                const quantity = $("#quantity").val();
                const data = {
                    bookId: bookId,
                    quantity: quantity,
                    cartId: response.id
                }

                $.ajax({
                    url: apiUrl + "/api/CartDetails/" + userId,
                    type: "POST",
                    contentType: 'application/json',
                    data: JSON.stringify(data),
                    success: function () {
                        Swal.fire({
                            icon: 'success',
                            title: 'The book has been added to the cart.',
                            showConfirmButton: false,
                            timer: 1500
                        })
                    },
                    error: function (error) {
                        console.log(error)
                    },
                })
            },
            error: function (error) {
                console.log(error)
            },
        })
    }
    else {
        window.location.href = "/Identity/Account/Login";
    }
}




