function TakeInformation() {
    let requestURL = "https://" + window.location.host + "/Home/TakeMyInform";
    fetch(requestURL)
        .then(response => response.json())
        .then(inform => AddInformationOnPage(inform));

}
function AddInformationOnPage(inform) {
    let allPhotos = document.querySelectorAll(".MyPhoto");
    let allNames = document.querySelector(".MyName")
    let allLinks = document.querySelector(".MyLink")
    console.log(allNames);
    for (let i = 0; i < allPhotos.length; i++) {
        allPhotos[i].innerHTML = '<img src=' + inform.Photo + '  class="pic_with_text_near dark-change-border" />';
    }
    for (let i = 0; i < allNames.length; i++) {
        allNames[i].innerText = inform.Email;
    }

}