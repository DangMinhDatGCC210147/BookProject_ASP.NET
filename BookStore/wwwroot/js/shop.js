//Show on Shop
$(window).on("load", function () {
    AjaxBarArea();
    AjaxAllBooks();
});

function AjaxBarArea() {
    $.ajax({
        url: apiUrl + "/api/Shops",
        method: "GET",
        success: function (data) {
            DisplayBarArea(data);
        },
        error: function (error) {
            console.log(error)
        },
    })
}

var label_title = document.getElementById("found");

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

function DisplayBarArea(data) {
    var filterGenres = data.genres;
    var filterPublishers = data.publishers;
    var filterLanguages = data.languages;
    var filterAuthors = data.authors;
    let filterGenre = "";
    let filterPublisher = "";
    let filterLanguage = "";
    let filterAuthor = "";

    //Genres
    filterGenres.forEach(item => {
        filterGenre += `<li><a href="#" onclick="PerformFilter(1, ${item.id})">> ${item.name} <span>${item.quantity} </span></a></li>`
    });
    document.getElementById("genres").innerHTML = `<h3 class="sidebar-title">Genres</h3>
                        <div class="sidebar-categories">
                            <ul>
                                ${filterGenre}
                            </ul>
                        </div>`
    //Publisher
    filterPublishers.forEach(item => {
        filterPublisher += `<li><a href="#" onclick="PerformFilter(2, ${item.id})">> ${item.name} <span>${item.quantity} </span></a></li>`
    });
    document.getElementById("publishers").innerHTML = `<h3 class="sidebar-title">Publishers</h3>
                        <div class="sidebar-categories">
                            <ul>
                                ${filterPublisher}
                            </ul>
                        </div>`

    //Languages
    filterLanguages.forEach(item => {
        filterLanguage += `<li><a href="#" onclick="PerformFilter(3, ${item.id})">> ${item.name} <span>${item.quantity} </span></a></li>`
    });
    document.getElementById("languages").innerHTML = `<h3 class="sidebar-title">Languages</h3>
                        <div class="sidebar-categories">
                            <ul>
                                ${filterLanguage}
                            </ul>
                        </div>`

    //Authors
    filterAuthors.forEach(item => {
        filterAuthor += `<li><a href="#" onclick="PerformFilter(4, ${item.id})">> ${item.name} <span>${item.quantity} </span></a></li>`
    });
    document.getElementById("authors").innerHTML = `<h3 class="sidebar-title">Authors</h3>
                        <div class="sidebar-categories">
                            <ul>
                                ${filterAuthor}
                            </ul>
                        </div>`
}

//Search
var grid_sidebar1 = document.getElementById("result_search_1");
var grid_sidebar2 = document.getElementById("result_search_2");
$("#search").on("input", function () {
    var searchName = document.getElementById("search").value;

    if (searchName == "") {
        AjaxAllBooks();
        return;
    }

    $.ajax({
        url: apiUrl + "/api/Products/Search/" + searchName,
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
});

function PerformFilter(filter_title, filter_name) {
    $.ajax({
        url: apiUrl + "/api/Shops/Filter?filterName=" + filter_title + "&filterId=" + filter_name,
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
                        <a href="/Home/Detail/${item.id}">
                            <img src="/img/product/book/${item.image}" alt="">
                        </a>
                        <div class="product-action">
                            <a class="animate-left" title="Wishlist">
                                 ${item.isFavorite == 1 ? '<i class="bi bi-suit-heart-fill" onclick="DeleteWishlist(' + item.id + ')"></i>' : '<i class="bi bi-suit-heart icon_heart_' + item.id +'" onclick="AddToWishlist(' + item.id +')"></i>'}
                            </a>
                            <a class="animate-top" title="Add To Cart" onclick="AddToCart(${item.id})">
                                <i class="pe-7s-cart"></i>
                            </a>
                            <a class="animate-right" title="Quick View" data-bs-toggle="modal" data-bs-target="#exampleModal" href="#">
                                <i class="pe-7s-look"></i>
                            </a>
                        </div>
                    </div>
                    <div class="product-content">
                    <input type='hidden' id='quantity' runat='server' value="1">
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
                        <div class="product-action-list-style">
                            <a class="animate-right" title="Quick View" data-bs-toggle="modal" data-bs-target="#exampleModal" href="#">
                                <i class="pe-7s-look"></i>
                            </a>
                        </div>
                    </div>
                    <div class="product-content-list">
                        <div class="product-list-info">
                            <h4><a href="/Home/Detail/${item.id}">${item.title}</a></h4>
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