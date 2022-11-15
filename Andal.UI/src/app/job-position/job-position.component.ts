import { Component, OnInit, ViewChild } from '@angular/core';
import { DxDataGridComponent } from 'devextreme-angular';
import { SharedService } from '../shared.service';

@Component({
  selector: 'app-job-position',
  templateUrl: './job-position.component.html',
  styleUrls: ['./job-position.component.scss']
})
export class JobPositionComponent implements OnInit {

  dataSource:any=[];
  titles:any=[];

  constructor(private service:SharedService) {}

  ngOnInit(): void {
    this.refreshJobPositionList();
  }
  

  refreshJobPositionList(){
    this.service.getJobPositionList().subscribe(data=>{
      this.dataSource=data;
    });
    this.service.getJobTitleList().subscribe(data=>{
      this.titles=data;
    });
  }

  @ViewChild('gridContainer') gridContainer!: DxDataGridComponent;

  setTitleValue(rowData: any, value: any): void {
    rowData.id = null;
    (<any> this).defaultSetCellValue(rowData, value);
  }

  save(e: any){
    var index = this.gridContainer.instance.getRowIndexByKey(e.positionId);

    console.log(index);
    console.log(e);

    if(index < 0)
    {
      index = 0;
    }

    var val = {positionId:e.positionId,
              code:this.gridContainer.instance.cellValue(index, "Code", e.value),
              name:this.gridContainer.instance.cellValue(index, "Name", e.value),
              jobTitleId:this.gridContainer.instance.cellValue(index, "Job Title", e.value)};

    console.log(val);

    if(val.code == null || val.name == null || val.jobTitleId == null)
    {
      alert("Code, Name and Job Title are required");
    }
    else
    {
      if(e.positionId)
      {
        this.service.updateJobPosition(val).subscribe(res=>{
          this.gridContainer.instance.saveEditData();
          this.refreshJobPositionList();
        }, (error : any) => alert(error.error));
      }
      else
      {
        this.service.addJobPosition(val).subscribe(res=>{
          this.gridContainer.instance.saveEditData();
          this.refreshJobPositionList();
        }, (error : any) => alert(error.error));
      }
    }
  }

  cancel(){
    this.gridContainer.instance.cancelEditData();
  }

  edit(e: any){
    const indexRow = this.gridContainer.instance.getRowIndexByKey(e.positionId);
    this.gridContainer.instance.editRow(indexRow);
  }

  delete(e: any){
    // const indexRow = this.gridContainer.instance.getRowIndexByKey(e.id);
    // this.gridContainer.instance.deleteRow(indexRow);
    if(confirm('Are you sure, you want to delete this record?')){
      console.log(e.positionId)
      this.service.deleteJobPosition(e.positionId).subscribe(res=>{
        this.refreshJobPositionList();
      }, (error : any) => alert(error.error));
    };
  }

  addRow(){
    this.gridContainer.instance.addRow();
  }

}
