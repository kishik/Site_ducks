// JavaScript source code
window.onscroll = function () {
    let scrolled = window.pageYOffset || document.documentElement.scrollTop;
    
    let scrollCoef = 0.001

    /*
    console.log(scrolled)
    */
    
    document.getElementById('menu').style.opacity = 1 - scrolled * scrollCoef

      
    if  (document.getElementById('menu').style.opacity < 0)
         document.getElementById('menu').style.left = '10000px'
  
  
    if (document.getElementById('menu').style.opacity >= 0)
         document.getElementById('menu').style.left = '0px'

}

