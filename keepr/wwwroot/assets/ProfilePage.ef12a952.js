import{u as p,A as s,v as K,x as A,_ as j,b as L,d as D,w as E,y as N,z,e as _,V as H,c as a,a as e,B as R,t as u,n as C,F as f,r as I,P as m,o as l,h as V,p as q,i as M,j as B}from"./index.52b00485.js";class O{async getAllProfiles(){}async getProfileById(o){const n=await p.get(`api/profiles/${o}`);s.activeProfile=n.data}async getVaultsForProfiles(o){const n=await p.get(`api/profiles/${o}/vaults`);s.profileVaults=n.data.map(t=>new K(t))}async getKeepsForProfiles(o){const n=await p.get(`api/profiles/${o}/keeps`);s.profileKeeps=n.data.map(t=>new A(t))}}const g=new O;const Q={setup(){async function r(){try{await g.getProfileById(t.params.id)}catch(i){m.error(i,"[getProfileById]")}}async function o(){try{await g.getKeepsForProfiles(t.params.id)}catch(i){m.error(i,"[getAccountKeeps]")}}async function n(){try{await g.getVaultsForProfiles(t.params.id)}catch(i){m.error(i,"[getAccountVaults]")}}L(()=>{r(),o(),n()}),D(()=>{}),E(()=>{});const t=N(),h=z();return{route:t,router:h,focusedBtn1:_(()=>s.focusedBtn1),keeps:_(()=>s.profileKeeps),account:_(()=>s.account),profile:_(()=>s.activeProfile),vaults:_(()=>s.profileVaults),toggleCreateForm(){s.vaultEditForm=!1},scrollDown(){s.focusedBtn1=!s.focusedBtn1}}},components:{VaultCard:H}},c=r=>(q("data-v-7f04d565"),r=r(),M(),r),G={class:"container animate__animated animate__fadeIn"},J={class:"row mt-3"},T={class:"col-md-12 text-center mt-2 position-relative"},U={class:"card border-0 animate__animated animate__fadeIn"},W=["src"],X={key:1,src:"https://quotefancy.com/media/wallpaper/3840x2160/1715705-Ashley-Smith-Quote-Life-is-full-of-beauty-Notice-it.jpg",class:"img-fluid coverImg bShadow rounded skeleton-loader"},Y={class:"card-img-overlay d-flex justify-content-center align-items-end"},Z={key:0,class:"text-center position-absolute top-50"},$=["src"],ee={key:1,class:"text-center position-absolute top-50"},te=c(()=>e("img",{class:"bShadow p-1 profilePic skeleton-loader"},null,-1)),oe=[te],se={class:"col-md-12 d-flex flex-column mt-5 justify-content-center align-items-center"},ae={class:"col-md-10 justify-content-end mt-5 d-flex px-3"},le={class:"btn-group dropstart"},ne={key:0,class:"mdi mdi-dots-horizontal ms-3 fs-1 selectable text-dark","data-bs-toggle":"dropdown","aria-expanded":"false"},de={class:"dropdown-menu rounded bg-info bShadow py-0 border-0"},ce=c(()=>e("li",{class:"dotHover rounded p-2 text-center","data-bs-toggle":"modal","data-bs-target":"#accountForm"}," Edit Account ",-1)),re={class:"markoOne text-dark"},ie={class:"text-center d-flex align-items-center no-select text-dark"},_e={class:"bShadow rounded p-1"},pe=c(()=>e("h1",{class:"mx-2 rounded-5 no-select"},"|",-1)),ue={class:"bShadow rounded p-1"},fe={class:"row"},me={class:"col-md-12 text-dark text-center"},ge=c(()=>e("h3",null,"Vaults",-1)),he=[ge],ve=c(()=>e("h3",null,"Keeps",-1)),be=[ve],ye={class:"collapse animate__animated animate__fadeInLeft show",id:"vaultCollapse"},ke=c(()=>e("div",{class:"col-md-12 border-bottom mt-2 mb-3 border-2 bg-dark"},null,-1)),we={class:"row"},xe={class:"collapse animate__animated animate__fadeInLeft",id:"keepCollapse"},Pe=c(()=>e("div",{class:"col-md-12 p-1 border-bottom border-dark mt-2 mb-3 border-3"},null,-1)),Ce={class:"bricks"},Ie=c(()=>e("div",null,null,-1));function Ve(r,o,n,t,h,i){var v,b,y,k,w,x,P;const S=B("VaultCard"),F=B("KeepCard");return l(),a(f,null,[e("div",G,[e("div",J,[e("div",T,[e("div",U,[(v=t.profile)!=null&&v.coverImg?(l(),a("img",{key:0,src:(b=t.profile)==null?void 0:b.coverImg,alt:" account cover Image",class:"img-fluid coverImg bShadow rounded"},null,8,W)):(l(),a("img",X)),e("div",Y,[(y=t.profile)!=null&&y.picture?(l(),a("div",Z,[e("img",{class:"bShadow p-1 profilePic",src:(k=t.profile)==null?void 0:k.picture,alt:""},null,8,$)])):(l(),a("div",ee,oe))])])]),e("div",se,[e("div",ae,[e("div",le,[((w=t.profile)==null?void 0:w.id)==((x=t.account)==null?void 0:x.id)?(l(),a("i",ne)):R("",!0),e("ul",de,[e("li",{class:"dotHover rounded p-2 text-center","data-bs-toggle":"modal","data-bs-target":"#vaultForm",onClick:o[0]||(o[0]=d=>t.toggleCreateForm()),title:"Profile "}," Add Vault "),ce])])]),e("h3",re,u((P=t.profile)==null?void 0:P.name),1),e("div",ie,[e("span",_e,u(t.vaults.length)+" Vaults",1),pe,e("span",ue,u(t.keeps.length)+" Keeps",1)])])]),e("div",fe,[e("div",me,[e("button",{onClick:o[1]||(o[1]=d=>t.scrollDown()),class:C(["btn text-dark",t.focusedBtn1==!1?"focusedBtn":""]),"data-bs-toggle":"collapse","data-bs-target":"#vaultCollapse"},he,2),e("button",{onClick:o[2]||(o[2]=d=>t.scrollDown()),class:C(["btn text-dark",t.focusedBtn1==!0?"focusedBtn":""]),"data-bs-toggle":"collapse","data-bs-target":"#keepCollapse"},be,2)]),e("div",ye,[ke,e("div",we,[(l(!0),a(f,null,I(t.vaults,d=>(l(),a("div",{class:"col-md-3",key:d.id},[V(S,{vault:d},null,8,["vault"])]))),128))])])]),e("div",xe,[Pe,e("div",Ce,[(l(!0),a(f,null,I(t.keeps,d=>(l(),a("div",{class:"my-3",key:d.id},[V(F,{keep:d},null,8,["keep"])]))),128))])])]),Ie],64)}const Se=j(Q,[["render",Ve],["__scopeId","data-v-7f04d565"]]);export{Se as default};
