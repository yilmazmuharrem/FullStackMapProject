
window.onload=initial;
var map, vectorLayer,vectorSource, xDeger,yDeger,locationDeger,girdi
var location;
function initial(){
 


vectorSource =new ol.source.Vector({wrapX: false});
vectorLayer =new ol.layer.Vector({source:vectorSource});

//HARİTA OLUŞTURMA KODLARI ....
map = new ol.Map({
  target: 'map',
  
  view: new ol.View({
    center: ol.proj.fromLonLat([36, 42]),
    zoom: 4
  }),
  layers: [
      new ol.layer.Tile({
        source: new ol.source.OSM()
      }),
      vectorLayer
    ],
});





// POİNTER AÇMA KODLARI
const draw =new ol.interaction.Draw({
  type :"Point",
  source : vectorSource
});
draw.setActive(false);
map.addInteraction(draw);
  
draw.on("drawend",(event)=>{
  draw.setActive(false);
  var coord = event.feature.getGeometry().getCoordinates();
  coord = ol.proj.transform(coord, 'EPSG:3857', 'EPSG:4326');
  xDeger = coord[0];
  yDeger = coord[1];
  fonkx(xDeger,yDeger);

})

// VERİ TABANINDAKİ VERİLERİ CONSOLA YAZDIRMA
const denemeButoon =document.getElementById("getLocation");
  denemeButoon.addEventListener("click",()=>{
    fetch("https://localhost:7287/api/Location",{
      method:"get",
  headers:{
  "Content-Type" :"application/json"
  }

    }).then(response=>{
      return response.json();
    }).then(data=>{
      console.log(data)
    }).catch(ex=>console.log(ex))


    });


    // POİNTER KAPATMA KODLARI
  const addButton =document.getElementById("navbar");
  addButton.addEventListener("click",()=>{
    draw.setActive(true);
   
})
    };
    
    //POST İŞLEMİ
    function f() {
      locationDeger=document.getElementById("girdi").value;
      location.Name=locationDeger;
      location.X=xDeger;
      location.Y=yDeger;

      fetch("https://localhost:7287/api/Location",{
        method:"post",
    headers:{
    "Content-Type" :"application/json"
    },
    body:JSON.stringify({
      Name:locationDeger,
      X:xDeger,
      Y:yDeger
    }),
      }).then(response=>{
        return response.json();
      }).then(location=>{
        console.log(location)
      }).catch(ex=>console.log(ex))
  
      console.log("başarılı");
      console.log(xDeger+"  "+yDeger+"  "+locationDeger);
      
  }

  //JSPANEL OLUŞTURMA
function fonkx(x,y){

  
  jsPanel.create({
      theme: 'dark',
      headerLogo: '<i class="fad fa-home-heart ml-2"></i>',
      headerTitle: "CBS",
      headerToolbar: '<span class="text-sm">Konum Sistemi</span>',
      footerToolbar: document.getElementById("xyz"),
  
      content: '<label id="etiket">Etiket</label><input type="text"  id="girdi"></br><button onclick="f()" > Kaydet </button></br>'+
      " X :"+x+"</br>  Y :"+y,
      
           panelSize: {
          width: () => { return Math.min(800, window.innerWidth*0.9);},
          height: () => { return Math.min(500, window.innerHeight*0.6);}
      },
 
      animateIn: 'jsPanelFadeIn',
      contentAjax: {
          url: 'docs/sample-content/sampleContentHome.html',
          done: (xhr, panel) => {
              panel.content.innerHTML = xhr.responseText;
              Prism.highlightAll();
          }
      },
      
      onwindowresize: true,
    sadas

})
 var sadas=$(document).ready( function () {
  $('#myTable').DataTable();
} );

}

