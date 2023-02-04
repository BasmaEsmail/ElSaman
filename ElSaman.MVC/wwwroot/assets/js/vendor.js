const sideMenu = document.getElementById("side");
const menuBtn = document.querySelector("#menu-bars");
const closeBtn = document.getElementById("close-btn");

function openMenu() {
    menuBtn.style.display = 'none'
    sideMenu.style.display = 'block'
    closeBtn.style.display = 'block'
}


function closeMenu() {
    sideMenu.style.display = 'none'
    closeBtn.style.display = 'none'
    menuBtn.style.display = 'block'
}