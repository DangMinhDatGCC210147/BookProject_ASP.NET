$(window).on("load", function () {
    const apiUrl = localStorage.getItem("apiUrl")
    $.ajax({
        url: apiUrl + "/api/Carts",
        method: "GET",
        success: function (data) {
            console.log(data)
            showAllBooks(data);
        },
        error: function (error) {
            console.log(error)
        },
    })
});

function showAllBooks(data) {
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
        filterGenre += `<li><a href="#">> ${item.name} <span>${item.quantity} </span></a></li>`
    });
    document.getElementById("genres").innerHTML = `<h3 class="sidebar-title">Genres</h3>
                        <div class="sidebar-categories">
                            <ul>
                                ${filterGenre}
                            </ul>
                        </div>`
    //Publisher
    filterPublishers.forEach(item => {
        filterPublisher += `<li><a href="#">> ${item.name} <span>${item.quantity} </span></a></li>`
    });
    document.getElementById("publishers").innerHTML = `<h3 class="sidebar-title">Publishers</h3>
                        <div class="sidebar-categories">
                            <ul>
                                ${filterPublisher}
                            </ul>
                        </div>`

    //Languages
    filterLanguages.forEach(item => {
        filterLanguage += `<li><a href="#">> ${item.name} <span>${item.quantity} </span></a></li>`
    });
    document.getElementById("languages").innerHTML = `<h3 class="sidebar-title">Languages</h3>
                        <div class="sidebar-categories">
                            <ul>
                                ${filterLanguage}
                            </ul>
                        </div>`

    //Authors
    filterAuthors.forEach(item => {
        filterAuthor += `<li><a href="#">> ${item.name} <span>${item.quantity} </span></a></li>`
    });
    document.getElementById("authors").innerHTML = `<h3 class="sidebar-title">Authors</h3>
                        <div class="sidebar-categories">
                            <ul>
                                ${filterAuthor}
                            </ul>
                        </div>`
}
