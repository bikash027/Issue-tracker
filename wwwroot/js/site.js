// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const addProject1= document.querySelector(".addProject1");
const addProject2= document.querySelector(".addProject2");
addProject1.addEventListener('click',()=>{
    console.log("click is happening");
    addProject1.style.display='none';
    addProject2.style.display= 'block';

});