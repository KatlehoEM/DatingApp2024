import { Component, OnInit } from '@angular/core';
import { MembersService } from '../_services/members.service';
import { Member } from '../_models/member';
import { Pagination } from '../_models/pagination';

@Component({
  selector: 'app-visits',
  templateUrl: './visits.component.html',
  styleUrls: ['./visits.component.css']
})
export class VisitsComponent implements OnInit {
  members: Member[] | undefined
  predicate =  "visited";
  pageNumber = 1;
  pageSize = 5;
  pagination: Pagination | undefined
  filterVisit = "all"
  constructor(private memberService: MembersService){}

  ngOnInit(): void {
    this.loadVisits()
  }

  loadVisits(){
    this.memberService.getVisits(this.predicate,this.pageNumber,this.pageSize, this.filterVisit).subscribe({
      next: response => {
        this.members = response.result
        this.pagination = response.pagination
      }
    })
  }

  pageChanged(event: any) {
    if (this.pageNumber !== event.page){
      this.pageNumber = event.page;
      this.loadVisits();
    }
  }

}
