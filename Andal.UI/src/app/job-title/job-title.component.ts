import { Component, OnInit, ViewChild } from '@angular/core';
import { DxDataGridComponent } from 'devextreme-angular';
import { SharedService } from '../shared.service';


@Component({
  selector: 'app-job-title',
  templateUrl: './job-title.component.html',
  styleUrls: ['./job-title.component.scss']
})
export class JobTitleComponent implements OnInit {

  constructor(private service:SharedService) { }

  sampleDatasource:any=[];

  ngOnInit(): void {
    this.refreshJobTitleList();
  }
  
  refreshJobTitleList(){
    this.service.getJobTitleList().subscribe(data=>{
      this.sampleDatasource=data;
    });
  }

  @ViewChild('gridContainer') gridContainer!: DxDataGridComponent;

  save(e: any){
    var index = this.gridContainer.instance.getRowIndexByKey(e.titleId);

    if(index < 0)
    {
      index = 0;
    }

    var val = {titleId:e.titleId,
              code:this.gridContainer.instance.cellValue(index, "Code", e.value),
              name:this.gridContainer.instance.cellValue(index, "Name", e.value)};

    if(val.code == null || val.name == null)
    {
      alert("Code and Name are required");
    }
    else
    {
      if(e.titleId)
      {
        this.service.updateJobTitle(val).subscribe(res=>{
          this.gridContainer.instance.saveEditData();
          this.refreshJobTitleList();
        }, (error : any) => alert(error.error));
      }
      else
      {
        this.service.addJobTitle(val).subscribe(res=>{
          this.gridContainer.instance.saveEditData();
          this.refreshJobTitleList();
        }, (error : any) => alert(error.error));
      }
    }
  }

  cancel(){
    this.gridContainer.instance.cancelEditData();
  }

  edit(e: any){
    const indexRow = this.gridContainer.instance.getRowIndexByKey(e.titleId);
    this.gridContainer.instance.editRow(indexRow);
  }

  delete(e: any){
    // const indexRow = this.gridContainer.instance.getRowIndexByKey(e.id);
    // this.gridContainer.instance.deleteRow(indexRow);
    if(confirm('Are you sure, you want to delete this record?')){
      this.service.deleteJobTitle(e.titleId).subscribe(data=>{
        this.refreshJobTitleList();
      }, (error : any) => alert(error.error));
    };
  }

  addRow(){
    this.gridContainer.instance.addRow();
  }
}