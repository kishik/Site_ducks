function searchDelete() {
    document.getElementById("example_0").innerHTML = '<button class="add_post"> Предложить новость</button></div>';

}


function search() {

    let text = document.getElementById("textSearch").value;
    let requestURL = "https://" + window.location.host + "/Home/Search";
    console.log(text);
    let clonedNode = document.getElementById("example_1").cloneNode(true);
    let AllText = clonedNode.querySelector("#example_6");
    
    searchDelete();
    function AddNewsOnPage1(news) {
        if ((news.length != 0)) {
            for (let i = 0; i < news.length; i++) {
                
                console.log(news[i].Text);
                
                AllText.innerHTML = news[i].Text;
                document.getElementById("example_0").appendChild(clonedNode);
                clonedNode = document.getElementById("example_1").cloneNode(true);
                AllText = clonedNode.querySelector("#example_6");
                
            }
            TakeInformation();
        }
    }
    
    //searchDelete();
    fetch(requestURL, {
        method: 'POST', // *GET, POST, PUT, DELETE, etc.
    
        body: JSON.stringify({ "Text": text }),
        headers: {
            "Content-Type": "application/json;charset=UTF-8",
        }
    })
        .then(response => response.json())
    .then(news => AddNewsOnPage1(news));
    
}
