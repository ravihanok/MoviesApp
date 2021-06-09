export const JWT_Module_Options ={
    config:{
        tokenGetter:()=>{
          return localStorage.getItem('token');
        }
    }
}