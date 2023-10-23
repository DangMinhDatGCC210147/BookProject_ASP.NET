//Search
var grid_sidebar1 = document.getElementById("result_search_1");
var grid_sidebar2 = document.getElementById("result_search_2");
var label_title = document.getElementById("found");

function Search() {
    var searchName = document.getElementById("search").value;

    if (searchName == "") {
        AjaxAllBooks();
        return;
    }

    $.ajax({
        url: apiUrl + "/api/Products/Search?name=" + searchName + "&userId=" + userId,
        method: "GET",
        success: function (response) {
            console.log(response)
            if (response == "") {
                label_title.innerHTML = `<p>Book Name "${searchName}" Not Found</p>`;
                grid_sidebar1.innerHTML = ``;
                grid_sidebar2.innerHTML = ``;
            } else {
                label_title.innerHTML = `<p>Book Name "${searchName}" Found of <span>${response.length}</span></p>`;
                ShowData(response);
            }
        },
        error: function (error) {
            console.log(error)
        },
    })
};

function ShowData(results) {
    SearchResults_1 = "";
    SearchResults_2 = "";

    results.forEach(item => {
        SearchResults_1 +=
            ` 
           <div class="col-lg-6 col-md-6 col-xl-3">
                <div class="product-wrapper mb-30">
                    <div class="product-img">
                        <a href="/Home/Detail/${item.id}">
                            <img src="${apiUrl}/${item.image}" alt="Book Image">
                        </a>
                        <div class="product-action">
                            <a class="animate-left" title="Wishlist">
                                 ${item.isFavorite == 1 ? '<i class="bi bi-suit-heart-fill" id="icon_heart_' + item.id + '" onclick="DeleteWishlist(' + item.id + ')"></i>' : '<i class="bi bi-suit-heart" id="icon_heart_' + item.id + '" onclick="AddToWishlist(' + item.id + ')"></i>'}
                            </a>
                            <a class="animate-top" title="Add To Cart" onclick="AddToCart(${item.id})">
                                <i class="pe-7s-cart"></i>
                            </a>
                        </div>
                    </div>
                    <div class="product-content">
                    <input type='hidden' id='quantity' runat='server' value="1">
                        <div class="product-rating-2" style="display: flex; justify-content:center; align-items:center;">${checkRate(item.rate, "", 5)}</div>
                        <h4><a href="#">${item.title}</a></h4>
                        <span>$${item.sellingPrice}</span>
                    </div>
                </div>
            </div>
        `
    })

    results.forEach(item => {
        SearchResults_2 +=
            `    <div class="col-lg-12 col-xl-6">
                <div class="product-wrapper mb-30 single-product-list product-list-right-pr mb-60">
                    <div class="product-img list-img-width">
                        <a href="/Home/Detail/${item.id}">
                            <img src="/img/product/book/${item.image}" alt="">
                        </a>
                        <span>hot</span>
                    </div>
                    <div class="product-content-list">
                        <div class="product-list-info">
                            <h4><a href="/Home/Detail/${item.id}">${item.title}</a></h4>
                            <div class="product-rating-2" style="display: flex; justify-content:start; align-items:left;">${checkRate(item.rate, "", 5)}</div>
                            <span href="/Home/Detail/${item.id}">${item.sellingPrice}</span>
                        </div>
                        <div class="product-list-cart-wishlist">
                            <div class="product-list-cart">
                                <a class="btn-hover list-btn-style" href="#">add to cart</a>
                            </div>
                            <div class="product-list-wishlist">
                                <a class="btn-hover list-btn-wishlist" href="#">
                                    ${item.isFavorite == 1 ? '<i class="bi bi-suit-heart-fill"></i>' : '<i class="bi bi-suit-heart"></i>'}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        `
    })

    grid_sidebar1.innerHTML = SearchResults_1;
    grid_sidebar2.innerHTML = SearchResults_2;

}

function AjaxAllBooks() {
    label_title.innerHTML = `<p>All Books</p>`;
    var userId = $("#userId").val();
    if (userId != "") {
        $.ajax({
            url: apiUrl + "/api/Shops/" + userId,
            method: "GET",
            success: function (response) {
                console.log(response)
                ShowData(response);
            },
            error: function (error) {
                console.log(error)
            },
        })
    } else {
        userId = "getAll";
        $.ajax({
            url: apiUrl + "/api/Shops/" + userId,
            method: "GET",
            success: function (response) {
                console.log(response)
                ShowData(response);
            },
            error: function (error) {
                console.log(error)
            },
        })
    }
}