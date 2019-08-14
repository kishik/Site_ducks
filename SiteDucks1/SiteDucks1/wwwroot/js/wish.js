
function wishh() {
    fetch("/home/getJSON").then(res => res.json()).then(res => publish(res));


    function publish(res) {

        let img = document.getElementById('wish');
        img.width = 200;
        img.height = 211;
        let rand = 1 + Math.random() * (15 + 1 - 1);
        let x = Math.floor(rand);

        console.log(res[x].Pic);
        img.innerHTML = '<img src=' + res[x].Pic + ' />';
    }

}


