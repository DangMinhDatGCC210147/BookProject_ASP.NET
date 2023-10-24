// Show Wishlist
$(window).on("load", function () {
    $.ajax({
        url: apiUrl + "/api/Wishlists/" + userId,
        type: "GET",
        contentType: 'application/json',
        success: function (response) {
            console.log(response)
            var row = "";
            response.forEach(item => {
                row +=
                `
                    <tr id="row_${item.bookId}">
                        <td class="product-remove"><a onclick="DeleteWishlist(${item.bookId})"><i class="pe-7s-close"></i></a></td>
                        <td class="product-thumbnail">
                            <a href="/Home/Detail/${item.bookId}"><img src="${apiUrl}/${item.image}" alt=""></a>
                        </td>
                        <td class="product-name_${response.bookId}"><a href="/Home/Detail/${item.bookId}">${item.title}</a></td>
                        <td class="product-price-cart"><span class="amount">$${item.sellingPrice}</span></td>
                    </tr>
                `
            })
            
            document.getElementById("listWishlist").innerHTML = row;
            AjaxWishlist();
        },
        error: function (error) {
            console.log(error)
        },
    })
});
