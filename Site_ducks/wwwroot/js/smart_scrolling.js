{
    
    let NumNews = 0;


    function init() {
        {
            let requestURL = "https://" + window.location.host + "/Home/GetNews";
            fetch(requestURL, {
                method: 'POST', // *GET, POST, PUT, DELETE, etc.

                body: JSON.stringify({ "Number": NumNews }),
                headers: {
                    "Content-Type": "application/json;charset=UTF-8",
                }
            })
                .then(response => response.json())
                .then(news => AddNewsOnPage(news));
        }
        window.onscroll = push_news;

    }
    
    function AddNewsOnPage(news) {
        if ((news.length != 0)) {
           for (let i = 0; i < news.length; i++) {
                let clonedNode = document.getElementById("example_1").cloneNode(true);


                //let AllLinksPictures = document.querySelectorAll("#example_2");
                //let AllImages = document.querySelectorAll("#example_3");
                //let AllNamesLinks = document.querySelectorAll("#example_4");
                //let AllTimeCodes = document.querySelectorAll("#example_5");
                let AllText = clonedNode.querySelector("#example_6");
                //AllLinksPictures[NumNews + 1].href = newNode[NumNews].Link;
                //AllImages[NumNews + 1].src = newNode[NumNews].Image;
                //AllNameLinks[NumNews + 1].href = news[NumNews].Link;
                //AllTimeCodes[NumNews + 1].src = news[NumNews].TimeCode;
                AllText.innerHTML = news[i].Text;
                document.getElementById("example_0").appendChild(clonedNode);
                NumNews++;
            }
            TakeInformation();
        }
    }
    function push_news(event) {
        
        if (document.documentElement.scrollHeight - window.pageYOffset < 1) {


            let requestURL = "https://" + window.location.host + "/Home/GetNews";
            fetch(requestURL, {
                method: 'POST', // *GET, POST, PUT, DELETE, etc.

                body: JSON.stringify({ "Number": NumNews }),
                headers: {
                    "Content-Type": "application/json;charset=UTF-8",
                }
            })
                .then(response => response.json())
                .then(news => AddNewsOnPage(news));

        }







    }
}


