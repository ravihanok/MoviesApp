import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GridApi } from 'ag-grid-community';
import { MovieModal } from '../movie-modal';
import { ButtonRendererComponent } from '../Renderer/button-renderer/button-renderer.component';
import { MovieService } from '../shared/movie.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  Movies:MovieModal[];
  gridApi:GridApi;
  columnDefs=[
    { field: 'language', sortable: true, filter: true, resizable: true },
    { field: 'location', sortable: true, filter: true, resizable: true },
    { field: 'plot', sortable: true, filter: true , resizable: true},
  //  { field: 'soundEffects', sortable: true, filter: true },
  //  { field: 'stills', sortable: true, filter: true },
    { field: 'title', sortable: true, filter: true , resizable: true},
    { field: 'imdbId', sortable: true, filter: true, resizable: true },
    { field: 'listingType', sortable: true, filter: true , resizable: true},
    { field: 'imdbRating', sortable: true, filter: true, resizable: true },
    {
      headerName: '',
      cellRenderer: 'buttonRenderer',
      cellRendererParams: {
        onClick: this.onViewDetails.bind(this),
        label: 'View'
      }
    },
];
  title = 'app';
  frameworkComponents: any;
  constructor(private movieService:MovieService,
    private route:ActivatedRoute,
    private router:Router
    ){
    this.frameworkComponents = {
      buttonRenderer: ButtonRendererComponent,
    }
  }
  ngOnInit(){
    this.movieService.getMoviesData().subscribe(res=>{
      this.Movies = res;
    });
  }
  onGridReady(params){
    this.gridApi = params.api;
    this.gridApi.sizeColumnsToFit();
  }
  onViewDetails(e){
    this.router.navigate(['/movies/'+e.rowData.id+'/details']);
  }
}
