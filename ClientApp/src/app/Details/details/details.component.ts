import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MovieModal } from 'src/app/movie-modal';
import { MovieService } from 'src/app/shared/movie.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  movie: MovieModal;
  constructor(private movierService:MovieService,
    private route:ActivatedRoute,
    private router:Router
    ) { }

  ngOnInit() {
    this.route.params.subscribe(x=>{
      this.movierService.getMovie(x.id).subscribe(res=>{
        this.movie = <MovieModal>res;
        //console.log(this.movie);
      })
    })
  }

}
