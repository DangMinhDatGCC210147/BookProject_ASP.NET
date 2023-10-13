const apiUrl = localStorage.getItem("apiUrl")
const userId = localStorage.getItem("userId")

// Cart Header
AjaxCallCart();
function AjaxCallCart() {
    $.ajax({
        url: apiUrl + "/api/CartDetails/" + userId,
        type: "GET",
        contentType: 'application/json',
        success: function (response) {
            console.log(response);
            document.getElementById("cart").innerHTML =
                `
                <a class="icon-cart" href="#">
				    <i class="ti-shopping-cart"></i>
				    <span class="shop-count book-count">${response.length}</span>
			    </a>
                <ul class="cart-dropdown" id="row_cart_dropdown"> </ul>   
            `;
            var row = "";
            response.forEach(item => {
                row += `
                            <li class="single-product-cart">
                                <div class="cart-img">
                                    <a href="/Home/Detail/${item.id}"><img src="/img/product/book/${item.image}" alt=""></a>
                                </div>
                                <div class="cart-title">
                                    <h5><a href="/Home/Detail/${item.id}"> ${item.title}</a></h5>
                                    <span>${item.quantity} x ${item.price}</span>
                                    <span class="subtotal">$${item.subTotal}</span>
                                </div>
                                <div class="cart-delete">
                                    <a onclick="DeleteCart(${item.bookId})"><i class="ti-trash"></i></a>
                                </div>
                            </li>
                        `

            })
            row +=
                `
                <li class="cart-space">
								<div class="cart-sub">
									<h4>Subtotal</h4>
								</div>
								<div class="cart-price" id="price">
									<h4>$0.00</h4>
								</div>
							</li >
                <li class="cart-btn-wrapper">
                    <a class="cart-btn btn-hover" href="/Home/Cart?userId=${userId}">view cart</a>
                    <a class="cart-btn btn-hover" asp-controller="Home" asp-action="CheckOut">checkout</a>
                </li>
            `
            document.getElementById("row_cart_dropdown").innerHTML = row;
            SubTotal();
        },
        error: function (error) {
            console.log(error)
        },
    });

    function SubTotal() {
        var elements = document.querySelectorAll("span.subtotal");
        var calculate = 0;
        for (var i = 0; i < elements.length; i++) {
            calculate += parseFloat(elements[i].textContent.replace('$', ''));
        }
        $("#price").text(calculate.toFixed(2));
        return calculate;
    }
}