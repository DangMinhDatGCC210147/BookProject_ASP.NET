/*const apiUrl = localStorage.getItem("apiUrl")

//Show on Shop
$(window).on("load", function () {    
    $.ajax({
        url: apiUrl + "/api/Home",
        method: "GET",
        success: function (response) {
            console.log(response)
            //SHOW GENRE
            // Get genre name
            console.log(response.topGenres)
            var listGenreName = getGenreName(response.topGenres, "", 0);
            document.getElementById("genre_name").innerHTML = listGenreName;

            var firstAnchor = document.querySelector('#genre_name a:first-child');
            firstAnchor.classList.add('active');
           
            // Get books in genre
            var listBooks = getBookByGenre(response.topGenres, "", 0);
            document.getElementById("books_in_genre").innerHTML = listBooks;

            var firstAnchor = document.querySelector('#books_in_genre div:first-child');
            firstAnchor.classList.add('active');

            // SHOW TOP AUTHOR (WRITER)
            document.getElementById("theBestAuthor").innerHTML =
                `
                    <div class="writer-wrapper">
                            <a href="/Home/Detail/${response.topAuthors[0].bookId}">
								<img src="/img/product/book/${response.topAuthors[0].bookImage}" alt="">
							</a>
                            <div class="writer-content">
                                 <input type='hidden' id='quantity' runat='server' value="1">
                                <h4>${response.topAuthors[0].bookTitle}</h4>
                                <span>${response.topAuthors[0].authorName}</span>
                            </div>
                        <div class="product-rating-2" style="display: flex; justify-content:center; align-items:center;">${checkRate(response.topAuthors[0].reviewRate, "", 5)}</div>
                    </div>
                `;

            let product_wrapper = "";
            for (var i = 1; i < response.topAuthors.length; i++) {
                product_wrapper += `
                        <div class="product-wrapper">
							<div class="product-img-2">
								<a href="/Home/Detail/${response.topAuthors[i].bookId}">
									<img src="/img/product/book/${response.topAuthors[i].bookImage}" alt="">
								</a>
								<div class="product-action-2">
									<a class="animate-left add-style-2" title="Add To Cart" onclick="AddToCart(${response.topAuthors[i].bookId})">Add to Cart <i class="ti-shopping-cart"></i></a>
									<a class="animate-right wishlist-style-2" title="wishlist" href="#">
										<i class="ti-heart"></i>
									</a>
								</div>
							</div>
							<div class="product-content-3 text-center">
								<h4><a href="#">${response.topAuthors[i].bookTitle}</a></h4>
								<div class="product-rating-2" style="display: flex; justify-content:center; align-items:center;">${checkRate(response.topAuthors[i].reviewRate, "", 5)}</div>
							</div>
						</div>
                `
            }

            document.getElementById("topAuthors").innerHTML = product_wrapper;

        },
        error: function (error) {
            console.log(error)
        },
    })
});*/

// TOP BOOK BY GENRE
function getBookByGenre(topGenres, listBooks, i) {
    if (topGenres.length - 1 == i) {
        return listBooks;
    }

    if (i == 0 || topGenres[i].genreName != topGenres[i - 1].genreName) {
        listBooks += "<div class=\"tab-pane show fade\" id=\"home" + topGenres[i].genreId + "\" role=\"tabpanel\">\n" +
            "    <div class=\"custom-row\">\n";  // div start
        listBooks += getBooks(topGenres, topGenres[i].genreId);
        listBooks += "</div> </div > "; // div end
    }
    return getBookByGenre(topGenres, listBooks, i + 1);
}

// GET GENRE NAME
function getGenreName(topGenres, listGenreName, i) {

    if (topGenres.length == i + 1) {
        return listGenreName;
    }

    if (i == 0 || topGenres[i].genreName != topGenres[i - 1].genreName) {
        listGenreName += "<a href=\"#home" + topGenres[i].genreId + "\" data-bs-toggle=\"tab\" role=\"tab\">\n" +
            "       <h4>" + topGenres[i].genreName + " </h4>\n" +
            "</a>";
    }

    return getGenreName(topGenres, listGenreName, i + 1);
}

// GET 8 BOOK
function getBooks(topGenres, genreId) {
    var book = "";
    for (var i = 0; i < topGenres.length; i++) {
        if (topGenres[i].genreId == genreId) {
            book += "<div class=\"custom-col-5 custom-col-style mb-95\">\n" +
                "        <div class=\"product-wrapper\">\n" +
                "         <div class=\"product-img-2\">\n" +
                "          <a href=\"Home/Detail/" + topGenres[i].bookId + "\">\n" +
                "           <img src=\"/img/product/book/" + topGenres[i].bookImage + "\" alt=\"\">\n" +
                "          </a>\n" +
                "          <div class=\"product-action-2\">\n" +
                "           <a class=\"animate-left add-style-2\" title=\"Add To Cart\" onclick=\"AddToCart(" + topGenres[i].bookId + ")\">Add to Cart <i class=\"ti-shopping-cart\"></i></a>\n" +
                "           <a class=\"animate-right wishlist-style-2\" title=\"wishlist\" onclick=\"AddToWishlist(" + topGenres[i].bookId + ")\">\n" +
                "            <i class=\"ti-heart\"></i>\n" +
                "           </a>\n" +
                "          </div>\n" +
                "         </div>\n" +
                "         <div class=\"product-content-2 text-center\">\n" +
                "              <input type='hidden' id='quantity' runat='server' value=\"1\">"  +
                "          <h4><a href=\"Home/Detail/" + topGenres[i].bookId + "\">" + topGenres[i].bookTitle + "</a></h4>\n" +
                "          <span>By: " + topGenres[i].authorName + "</span>\n" +
                "                   <div class=\"product-rating-2\" style=\"display: flex; justify-content:center; align-items:center;\">" + checkRate(topGenres[i].reviewRate, "", 5) + "</div>\n" +
                "         </div>\n" +
                "        </div>\n" +
                "       </div>";
        }
    }
    return book;
}

