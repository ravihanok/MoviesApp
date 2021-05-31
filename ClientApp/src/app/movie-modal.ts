import { Stills } from "./shared/stills";

export interface MovieModal{
    id:number;
    language:string;
    location:string;
    plot:string;
    poster:string;
    soundEffects:[];
    stills:Stills[];
    title:string;
    imdbID:string;
    listingType:string;
    imdbRating:string
}
