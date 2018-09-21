import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-ranking',
  templateUrl: './ranking.component.html',
  styleUrls: ['./ranking.component.css']
})
export class RankingComponent implements OnInit {
  columns: string[];
  rankings: any;

  constructor(private http:HttpClient) { }

  ngOnInit() {
    this.columns = this.getColumns();
    this.getRankings();
  }

  getRankings() {
    this.http.get('http://localhost:5000/api/rankings').subscribe(response => {
      this.rankings = response;
    }, error => {
      console.log(error);
    });
  }

  getColumns(): string[]{
    return ['seqNum', 'userNum', 'totalScore', 'rankNum', 'createTime']};
  }
}
