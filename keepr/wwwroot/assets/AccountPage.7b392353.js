import{_ as K,b as F,d as H,w as P,e as r,A as d,V as j,c as o,a as t,t as _,n as w,F as u,r as y,f as C,P as V,g as D,o as s,h as S,p as E,i as L,j as A}from"./index.52b00485.js";const N={setup(){async function l(){try{await C.getAccountKeeps()}catch(i){V.error(i,"[getAccountKeeps]")}}async function c(){try{await C.getAccountVaults()}catch(i){V.error(i,"[getAccountVaults]")}}return F(()=>{l(),c()}),H(()=>{}),P(()=>{}),{focusedBtn1:r(()=>d.focusedBtn1),keeps:r(()=>d.accountKeeps),account:r(()=>d.account),vaults:r(()=>d.accountVaults),toggleCreateForm(){d.activeVault={},d.vaultEditForm=!1},scrollDown(){d.focusedBtn1=!d.focusedBtn1}}},components:{VaultCard:j}},a=l=>(E("data-v-85688437"),l=l(),L(),l),z={class:"container animate__animated animate__fadeIn"},O={class:"row justify-content-center mt-3"},M={class:"col-md-10 text-center mt-2 position-relative"},q={class:"card border-0 animate__animated animate__fadeIn"},G=["src"],J={key:1,src:"",class:"img-fluid coverImg bShadow rounded skeleton-loader"},Q={class:"card-img-overlay d-flex justify-content-center align-items-end"},R={key:0,class:"text-center position-absolute top-50"},T=["src"],U={key:1,class:"text-center position-absolute top-50"},W=a(()=>t("img",{class:"bShadow p-1 profilePic skeleton-loader"},null,-1)),X=[W],Y={class:"col-md-12 d-flex flex-column justify-content-center mt-5 mt-md-2 align-items-center"},Z={class:"col-md-10 justify-content-end d-flex mt-5 mt-md-3 px-3"},$=D('<div class="p-2" data-v-85688437><img class="dotHover rounded-circle" src="https://cdn-icons-png.flaticon.com/512/1377/1377257.png" alt="pinterest icon" height="40" width="40" title="share to pinterest" data-v-85688437><img class="dotHover rounded-5 ms-2" src="https://cdn-icons-png.flaticon.com/512/2504/2504903.png" alt="facebook icon" height="40" width="40" title="share to facebook" data-v-85688437><img class="dotHover rounded-4 ms-2" src="https://cdn-icons-png.flaticon.com/512/1409/1409946.png" alt="instagram icon" height="40" width="40" title="share to instagram" data-v-85688437><img class="dotHover rounded-circle ms-2" src="https://cdn-icons-png.flaticon.com/512/552/552486.png" alt="email icon" height="40" width="40" title="share through email" data-v-85688437></div>',1),tt={class:"btn-group dropstart"},et=a(()=>t("i",{class:"mdi mdi-dots-horizontal ms-3 fs-1 px-1 dotHover rounded selectable text-dark","data-bs-toggle":"dropdown","aria-expanded":"false"},null,-1)),ot={class:"dropdown-menu rounded bg-info bShadow py-0 border-0"},st=a(()=>t("li",{class:"dotHover rounded p-2 text-center","data-bs-toggle":"modal","data-bs-target":"#accountForm"}," Edit Account ",-1)),at={class:"markoOne text-dark no-select"},ct={class:"markoOne text-secondary no-select"},nt={class:"text-center d-flex align-items-center no-select text-dark"},dt={class:"bShadow rounded p-1"},lt=a(()=>t("h1",{class:"mx-2 rounded-5 no-select"},"|",-1)),it={class:"bShadow rounded p-1"},rt={class:"row"},_t={class:"col-md-12 text-dark text-center"},ut=a(()=>t("h3",null,"Vaults",-1)),mt=[ut],pt=a(()=>t("h3",null,"Keeps",-1)),ht=[pt],gt={class:"collapse animate__animated animate__fadeInLeft show",id:"vaultCollapse"},vt=a(()=>t("div",{class:"col-md-12 border-bottom mt-2 mb-3 border-2 bg-dark"},null,-1)),ft={class:"row mb-4"},bt={class:"collapse animate__animated animate__fadeInLeft",id:"keepCollapse"},kt=a(()=>t("div",{class:"col-md-12 p-1 border-bottom border-dark mt-2 mb-3 border-3"},null,-1)),xt={class:"bricks mb-4"},wt=a(()=>t("div",null,null,-1));function yt(l,c,i,e,Ct,Vt){var m,p,h,g,v,f,b,k,x;const I=A("VaultCard"),B=A("KeepCard");return s(),o(u,null,[t("div",z,[t("div",O,[t("div",M,[t("div",q,[(m=e.account)!=null&&m.coverImg?(s(),o("img",{key:0,src:(p=e.account)==null?void 0:p.coverImg,alt:" account cover Image",class:"img-fluid coverImg bShadow rounded"},null,8,G)):(s(),o("img",J)),t("div",Q,[(h=e.account)!=null&&h.picture?(s(),o("div",R,[t("img",{class:"bShadow p-1 profilePic",src:(g=e.account)==null?void 0:g.picture,alt:""},null,8,T)])):(s(),o("div",U,X))])])]),t("div",Y,[t("div",Z,[$,t("div",tt,[et,t("ul",ot,[t("li",{class:"dotHover rounded p-2 text-center","data-bs-toggle":"modal","data-bs-target":"#vaultForm",onClick:c[0]||(c[0]=n=>e.toggleCreateForm())}," Add Vault "),st])])]),t("h3",at,_((f=(v=e.account)==null?void 0:v.name)==null?void 0:f.split("@")[0]),1),t("h6",ct,_((b=e.account)==null?void 0:b.email),1),t("div",nt,[t("span",dt,_((k=e.vaults)==null?void 0:k.length)+" Vaults",1),lt,t("span",it,_((x=e.keeps)==null?void 0:x.length)+" Keeps",1)])])]),t("div",rt,[t("div",_t,[t("button",{onClick:c[1]||(c[1]=n=>e.scrollDown()),class:w(["btn text-dark",e.focusedBtn1==!1?"focusedBtn":""]),"data-bs-toggle":"collapse","data-bs-target":"#vaultCollapse",title:"toggle vaults"},mt,2),t("button",{onClick:c[2]||(c[2]=n=>e.scrollDown()),class:w(["btn text-dark",e.focusedBtn1==!0?"focusedBtn":""]),"data-bs-toggle":"collapse","data-bs-target":"#keepCollapse",title:"toggle keeps"},ht,2)]),t("div",gt,[vt,t("div",ft,[(s(!0),o(u,null,y(e.vaults,n=>(s(),o("div",{class:"col-md-3",key:n.id},[S(I,{vault:n},null,8,["vault"])]))),128))])])]),t("div",bt,[kt,t("div",xt,[(s(!0),o(u,null,y(e.keeps,n=>(s(),o("div",{class:"my-3",key:n.id},[S(B,{keep:n},null,8,["keep"])]))),128))])])]),wt],64)}const At=K(N,[["render",yt],["__scopeId","data-v-85688437"]]);export{At as default};
