let main = document.querySelector("main")
let button =document.querySelector("button")
let btns = document.querySelectorAll(".link")
let nav = document.querySelector("nav")
let header = document.querySelector("header")
let listado = document.getElementById("listado")
let h2= document.querySelector("h2")
let div= document.querySelector("div")

//window.onload=function(){alert('Bienvenido a esta pagina');}

/* esta funcionla voy a usar para la muestra de novedades
mientras se realiza la busqueda

function alerta(){
alert('mensaje');
}
window.onload = function(){
setTimeout('alerta()',5000);
}

*/

button.addEventListener("click",()=>{
    nav.classList.toggle("abierto")
    header.classList.toggle("header-abierto")
})

button.addEventListener("click",()=>{
    if(listado.innerHTML=="menu_open"){
        listado.innerHTML="menu"
        }else{
            listado.innerHTML="menu_open"
        }   
    })


button.addEventListener("click",()=>{
if(h2.innerHTML=="BiblioteccAccenturE"){
    h2.innerHTML="Menu"
    }else{
        h2.innerHTML="BiblioteccAccenturE" 
    }
})

btns.forEach(boton=>{
    boton.addEventListener("click",e=>{
        e.preventDefault()
        ajax(`${e.target.dataset.archivo}.html`)
        nav.classList="0"
        header.classList="0"
        if(h2.innerHTML=="Menu"){
        h2.innerHTML="BiblioteccAccenturE"
        }else{
            h2.innerHTML="Menu"
        }
        if(listado.innerHTML=="menu"){
            listado.innerHTML="menu_open"
            }else{
                listado.innerHTML="menu"
            } 
    })  
})


function ajax(url){
    
    let xhr = new XMLHttpRequest
    xhr.open("GET",url)
    xhr.addEventListener("readystatechange",()=>{
        if(xhr.readyState==4&&xhr.status==200){         
            main.innerHTML=xhr.response            
        }     
    })
    xhr.send()
}

item = document.querySelectorAll("#item").values;



