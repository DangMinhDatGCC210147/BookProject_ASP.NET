/*const apiUrl = localStorage.getItem("apiUrl")

//Show on Shop
$(window).on("load", function () {
    AjaxShowBestSelling();
    AjaxShowTopPublisher();
});

function AjaxShowBestSelling() {
    ShowGenre();
}

function ShowGenre() {
    $.ajax({
        url: apiUrl + "/api/Shops",
        method: "GET",
        success: function (data) {

        },
        error: function (error) {
            console.log(error)
        },
    })
}


function AjaxShowTopPublisher() {
    $.ajax({
        url: apiUrl + "/api/Products",
        method: "GET",
        success: function (data) {
            ShowData(data);
        },
        error: function (error) {
            console.log(error)
        },
    })
}

function ShowData(results) {
    SearchResults_1 = "";
    SearchResults_2 = "";

    results.forEach(item => {
        SearchResults_1 +=
            ` 
           <div class="col-lg-6 col-md-6 col-xl-3">
                <div class="product-wrapper mb-30">
                    <div class="product-img">
                        <a href="/Detail/${item.id}">
                            <img src="/img/product/book/${item.image}" alt="">
                        </a>
                        <div class="product-action">
                            <a class="animate-left" title="Wishlist" href="#">
                                <i class="pe-7s-like"></i>
                            </a>
                            <a class="animate-top" title="Add To Cart" href="#">
                                <i class="pe-7s-cart"></i>
                            </a>
                            <a class="animate-right" title="Quick View" data-bs-toggle="modal" data-bs-target="#exampleModal" href="#">
                                <i class="pe-7s-look"></i>
                            </a>
                        </div>
                    </div>
                    <div class="product-content">
                        <h4><a href="#">${item.title}</a></h4>
                        <span>${item.sellingPrice}</span>
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
                        <a href="/Detail/${item.id}">
                            <img src="/img/product/book/${item.image}" alt="">
                        </a>
                        <span>hot</span>
                        <div class="product-action-list-style">
                            <a class="animate-right" title="Quick View" data-bs-toggle="modal" data-bs-target="#exampleModal" href="#">
                                <i class="pe-7s-look"></i>
                            </a>
                        </div>
                    </div>
                    <div class="product-content-list">
                        <div class="product-list-info">
                            <h4><a href="#">${item.title}</a></h4>
                            <span>${item.sellingPrice}</span>
                            <p>Lorem ipsum dolor sit amet, mana consectetur adipisicing elit, sed do eiusmod tempor labore. </p>
                        </div>
                        <div class="product-list-cart-wishlist">
                            <div class="product-list-cart">
                                <a class="btn-hover list-btn-style" href="#">add to cart</a>
                            </div>
                            <div class="product-list-wishlist">
                                <a class="btn-hover list-btn-wishlist" href="#">
                                    <i class="pe-7s-like"></i>
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
*/