const apiUrl = localStorage.getItem("apiUrl")
const userId = localStorage.getItem("userId")

// Cart Header
if (userId) {
    AjaxCart();
    AjaxWishlist();
}

//Display cart in header
function AjaxCart() {
    $.ajax({
        url: apiUrl + "/api/CartDetails/" + userId,
        type: "GET",
        contentType: 'application/json',
        success: function (response) {
            document.getElementById("cart").innerHTML =
                `
                <a class="icon-cart" href="#">
				    <i class="ti-shopping-cart"></i>
				    <span class="shop-count book-count cart_count">${response.length}</span>
			    </a>
                <ul class="cart-dropdown" id="row_cart_dropdown"> </ul>   
            `;
            var row = "";

            //Cart empty
            if (response.length != 0) {
                response.forEach(item => {
                    row += `
                            <li class="single-product-cart" id="row_quickcart_${item.bookId}">
                                <div class="cart-img">
                                    <a href="/Home/Detail/${item.bookId}"><img src="${apiUrl}/${item.image}" alt=""></a>
                                </div>
                                <div class="cart-title">
                                    <h5><a id="product-name_${item.bookId}" href="/Home/Detail/${item.bookId}"> ${item.title}</a></h5>
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
                    <a class="cart-btn btn-hover" href="/Home/Cart">view cart</a>
                </li>
            `
            } else {
                row +=
                    `
                    <div id="cartEmpty">
						<img src="/img/cart/empty.gif"
							 style=" display: block; margin-left: auto; margin-right: auto; width: 50%;"/>
						<p class="text-center"
						   style=" display: block; margin-left: auto; margin-right: auto; ">
							Cart is empty
						</p>
					</div>
					<div class="pt-5">
						<h6 class="mb-3 back">
							<a href="/Home/Shop" class="text-body fw-semibold">
								<i class="fas fa-long-arrow-alt-left ms-5 me-2"></i>
								Go to shop
							</a>
						</h6>
					</div>
                `
            }
            document.getElementById("row_cart_dropdown").innerHTML = row;
            UpdateCartNumber();
        },
        error: function (error) {
            console.log(error)
        },
    });
}

// Add to cart
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
                            title: 'Added successfully!',
                            showConfirmButton: false,
                            timer: 800
                        })
                        AjaxCart();
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

// Delete Cart
function DeleteCart(bookId) {
    Swal.fire({
        title: 'Are you sure?',
        text: 'Book \"' + $("#product-name_" + bookId).text() + '\" will be removed in your cart',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                icon: 'success',
                title: 'Book \"' + $("#product-name_" + bookId).text() + '\" was removed in your cart',
                showConfirmButton: false,
                timer: 1000
            })

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
                    document.getElementById("row_quickcart_" + bookId).remove();

                    // Check is exist cart
                    if (document.getElementById("row_cart_" + bookId)) {
                        document.getElementById("row_cart_" + bookId).remove();
                    }
                    UpdateCartNumber();
                },
                error: function (xhr, status, error) {
                    console.log(xhr)
                    Swal.fire('Error!', 'An error occurred while deleting the record.', 'error');
                }
            });
        }
    });
}

//Display wishlist in header
function AjaxWishlist() {
    $.ajax({
        url: apiUrl + "/api/Wishlists/" + userId,
        type: "GET",
        success: function (response) {
            document.getElementById("wishlist").innerHTML =
                `<div class="header-cart-2">
                    <a class="icon-cart" href="#">
                        <i class="ti-heart"></i>
                        <span class="shop-count book-count wishlist_count">${response.length}</span>
                    </a>
                    <ul class="cart-dropdown" id="row_wishlist_dropdown">
                    </ul>
                </div>`;
            var row = "";
            //Cart empty
            if (response.length == 0) {
                row +=
                    `
                    <div id="cartEmpty">
						<img src="/img/cart/empty.gif"
							 style=" display: block; margin-left: auto; margin-right: auto; width: 50%;"/>
						<p class="text-center"
						   style=" display: block; margin-left: auto; margin-right: auto; ">
							Wishlist is empty
						</p>
					</div>
					<div class="pt-5">
						<h6 class="mb-3 back">
							<a href="/Home/Shop" class="text-body fw-semibold">
								<i class="fas fa-long-arrow-alt-left ms-5 me-2"></i>
								Go to shop
							</a>
						</h6>
					</div>
                `
            } else {
                response.forEach(item => {
                    row += `
                            <li class="single-product-wishlist" id="row_wishlist_${item.bookId}">
                                <div class="cart-img">
                                    <a href="/Home/Detail/${item.bookId}"><img src="${apiUrl}/${item.image}" alt=""></a>
                                </div>
                                <div class="cart-title">
                                    <h5><a id="product-name_${item.bookId}" href="/Home/Detail/${item.bookId}"> ${item.title}</a></h5>
                                    <span>$${item.sellingPrice}</span>
                                </div>
                                <div class="cart-delete">
                                    <a onclick="DeleteWishlist(${item.bookId})"><i class="ti-trash"></i></a>
                                </div>
                            </li>
                        `

                })
                row +=
                    `
                <li class="cart-btn-wrapper">
						<a class="cart-btn btn-hover" href="/Home/Wishlist?userId=${userId}">view wishlist</a>
				</li>
            `
            }
            document.getElementById("row_wishlist_dropdown").innerHTML = row;
        },
        error: function (error) {
            console.log(error)
        },
    })
}

// add to wishlist
function AddToWishlist(bookId) {

    if (userId) {
        const data = {
            bookId: bookId,
            userId: userId
        }
        $.ajax({
            url: apiUrl + "/api/Wishlists",
            type: "POST",
            data: JSON.stringify(data),
            contentType: 'application/json',
            success: function (response) {
                Swal.fire({
                    icon: 'success',
                    title: 'The book has been added to the wishlist.',
                    showConfirmButton: false,
                    timer: 1000
                })
                var icon = document.getElementById("wishlist_book_" + bookId);
                var icon2 = document.getElementById("wishlist_book_2_" + bookId);

                if ((icon.className == "bi bi-suit-heart") || (icon2.className == "bi bi-suit-heart")) {
                    // Remove old icon
                    icon.classList.remove("bi-suit-heart");               
                    // Add new icon
                    icon.classList.add("bi-suit-heart-fill");

                    if (icon2) {
                        icon2.classList.remove("bi-suit-heart");
                        icon2.classList.add("bi-suit-heart-fill");
                    }                    

                    // Remove the old click event handler
                    $("#wishlist_book_" + bookId).off("click");
                    $("#wishlist_book_2_" + bookId).off("click");

                    // Add a new click event handler
                    $("#wishlist_book_" + bookId).on("click", function () {
                        DeleteWishlist(bookId);
                    });
                    $("#wishlist_book_2_" + bookId).on("click", function () {
                        DeleteWishlist(bookId);
                    });
                }

                AjaxWishlist();
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

// Delete Wishlist
function DeleteWishlist(bookId) {
    Swal.fire({
        title: 'Are you sure?',
        text: 'Book \"' + $("#product-name_" + bookId).text() + '\" will be removed in your Wishlist',
        icon: 'warning',
        showConfirmButton: true,
        confirmButtonText: 'Yes, delete it!',
        showCancelButton: true, // Hiển thị nút "Cancel"
        cancelButtonText: 'No, cancel',
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire('Deleted!', 'Book \"' + $("#product-name_" + bookId).text() + '\" was removed in your Wishlist', 'success');

            const data = {
                bookId: bookId,
                newQuantity: 0,
                userId: userId
            }

            $.ajax({
                type: 'DELETE',
                url: apiUrl + '/api/Wishlists',
                data: JSON.stringify(data),
                contentType: 'application/json',
                success: function () {
                    var icon = document.getElementById("wishlist_book_" + bookId);
                    var icon2 = document.getElementById("wishlist_book_2_" + bookId);

                    if ((icon2.className == "bi bi-suit-heart-fill") || (icon.className == "bi bi-suit-heart-fill")) {
                        // Remove old icon
                        icon.classList.remove("bi-suit-heart-fill");
                        // Add new icon
                        icon.classList.add("bi-suit-heart");   

                        if (icon2) {
                            icon2.classList.remove("bi-suit-heart-fill");
                            icon2.classList.add("bi-suit-heart");
                        }                                             

                        // Remove the old click event handler
                        $("#wishlist_book_" + bookId).off("click");          
                        $("#wishlist_book_2_" + bookId).off("click");          

                        // Add a new click event handler
                        $("#wishlist_book_" + bookId).on("click", function () {    
                            AddToWishlist(bookId);
                        });
                        $("#wishlist_book_2_" + bookId).on("click", function () {    
                            AddToWishlist(bookId);
                        });

                    }

                    $('#row_wishlist_' + bookId).remove();
                    UpdateWishlistNumber();
                },
                error: function (xhr, status, error) {
                    console.log(xhr)
                    Swal.fire('Error!', 'An error occurred while deleting the record.', 'error');
                }
            });
        }
    });
}

function UpdateCartNumber() {
    var countElements = document.querySelectorAll(".single-product-cart");
    $(".cart_count").text(countElements.length);

    var subtotalElements = document.querySelectorAll(".subtotal");
    var total = 0;
    for (var i = 0; i < subtotalElements.length; i++) {
        total += parseFloat(subtotalElements[i].textContent.replace('$', ''));

        $("#price").text(total.toFixed(2));
        $("#subtotal").text(total.toFixed(2));
        $("#total").text(total.toFixed(2));
    }

    if (subtotalElements.length == 0) {
        AjaxCart();
    }
}

function UpdateWishlistNumber() {
    var countElements = document.querySelectorAll(".single-product-wishlist");
    console.log(countElements.length);
    $(".wishlist_count").text(countElements.length);
}

function ViewDetail(bookId) {
    window.location.href = "/Home/Detail/" + bookId;
}

// Display rate

function DisplayRate(rate, bookId) {
    console.log(checkRate(rate, "", 5));
    document.getElementById("rate_book_" + bookId).innerHTML = checkRate(rate, "", 5);
    document.getElementById("rate_book_id_" + bookId).innerHTML = checkRate(rate, "", 5);
}

function checkRate(rate, star, count) {
    if (count == 0) { return star; }

    if (rate <= 0) { star += "<i class=\"bi bi-star\" style=\"color: gold;\"></i>"; }
    else if (rate < 1) { star += "<i class=\"bi bi-star-half\" style=\"color: gold;\"></i>"; }
    else star += "<i class=\"bi bi-star-fill\" style=\"color: gold; background - color: gold; \"></i>";
    return checkRate(rate - 1, star, count - 1);
}