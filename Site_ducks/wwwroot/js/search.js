function searchDelete() {
    document.getElementById("example_0").innerHTML = '<button class="add_post"> Предложить новость</button></div>';

}


function search() {

    let text = document.getElementById("textSearch").value;
    let requestURL = "https://" + window.location.host + "/Home/Search";
    console.log(text);
    //searchDelete();
    fetch(requestURL, {
        method: 'POST', // *GET, POST, PUT, DELETE, etc.
    
        body: JSON.stringify({ "Text": text }),
        headers: {
            "Content-Type": "application/json;charset=UTF-8",
        }
    })
        .then(response => response.json())
    .then(news => AddNewsOnPage(news));
    
}
