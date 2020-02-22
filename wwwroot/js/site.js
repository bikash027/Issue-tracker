// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const addProject1= document.querySelector(".addProject1");
const addProject2= document.querySelector(".addProject2");
if(addProject1){
    addProject1.addEventListener('click',()=>{
        console.log("click is happening");
        addProject1.style.display='none';
        addProject2.style.display= 'block';

    });
}

const addUser1= document.querySelector(".addUser1");
const addUser2= document.querySelector(".addUser2");
if(addUser1){
    addUser1.addEventListener('click',()=>{
        console.log("click is happening");
        addUser1.style.display='none';
        addUser2.style.display= 'inline-block';

    });
}
const userNames=document.getElementById("userName");
const userIds=document.getElementById("userId");
if(userNames){
    userNames.addEventListener('click',()=>{
        userIds.selectedIndex=userNames.selectedIndex;
    });
}
if(userIds)
    userIds.style.display="none";

const task=document.getElementsByClassName("task");
for(let i=0;i<task.length;i++){
    task[i].firstElementChild.style.visibility='hidden';
    task[i].addEventListener('mouseover',()=>{
        // console.log("mouseover");
        // task[i].style.visibility='visible';
        task[i].firstElementChild.style.visibility='visible';
        task[i].style.backgroundColor='rgba(40, 81, 100, 0.555)';
    })
    task[i].addEventListener('mouseleave',()=>{
        // console.log("mouseleave");
        task[i].firstElementChild.style.visibility='hidden';
        task[i].style.backgroundColor='rgba(40, 81, 100, 0.0)';
    })
}

const moveAheads=[...document.getElementsByClassName('moveAhead')];
// console.log(moveAheads);
const specialInputs=moveAheads.concat([...document.getElementsByClassName('moveBack')]);
specialInputs.map((input)=>{
    const stylesParent=window.getComputedStyle(input.parentElement.parentElement);
    const stylesTask=window.getComputedStyle(task[0]);
    const width=parseInt(stylesParent.getPropertyValue('width'));
    const height=parseInt(stylesTask.getPropertyValue('height'));
    console.log(width,height);
    input.style.top=(height/2-25)+'px';
    input.style.left=(width/2-25)+'px';
})


const projectUsers=document.getElementsByClassName("projectUser");
for(let i=0;i<projectUsers.length;i++){
    const p=projectUsers[i].lastElementChild;
    p.style.display='none';
    projectUsers[i].addEventListener('mouseover',()=>{
        p.style.display='block';
    });
    projectUsers[i].addEventListener('mouseleave',()=>{
        p.style.display='none';
    });
}