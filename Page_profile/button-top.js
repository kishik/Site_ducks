

window.onload = function () 
{
	
	function add_mini_menu()
	{
		dialog.show()
	}
	function close_mini_menu ()
	{
		dialog.close()
	}
	
	let dialog = document.querySelector('dialog');
	
	document.querySelector('.menu-btn').addEventListener("mousemove", add_mini_menu)
	document.querySelector('#close').addEventListener("click", close_mini_menu, false)



}


