function TakeInformation() {
    let requestURL = "https://" + window.location.host + "/Home/TakeMyInform";
    fetch(requestURL)
        .then(response => response.json())
        .then(inform => );

}
function AddInformationOnPage(inform) {
    document.querySelectorAll(".MyPhoto");

}