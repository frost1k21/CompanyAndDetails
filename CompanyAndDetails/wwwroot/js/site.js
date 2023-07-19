// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

let selectedEmployee;
let employeesArr;

function getCompanyIdFromAttribute() {
  return document.querySelector("[data-companyId]").getAttribute("data-companyId");
}

async function getCompanyData() {
  const id = getCompanyIdFromAttribute();
  const companyResponse = await fetch(`http://localhost:5000/api/company-details?id=${id}`);
  return  await companyResponse.json();
}

function populateCompanyDetails(company) {
  document.querySelector("input[name='company-id']").value = company.companyId;
  document.querySelector("input[name='name']").value = company.companyName;
  document.querySelector("input[name='address']").value = company.address;
  document.querySelector("input[name='city']").value = company.city;
  document.querySelector("input[name='state']").value = company.state;
}

function populateHistory(orders) {
  const historySection = document.querySelector("[data-orders]")
  for (const order of orders) {
    const dateElem = document.createElement('p');
    const storeElem = document.createElement('p');
    dateElem.innerText = order.orderDate;
    storeElem.innerText = order.storeCity;
    historySection.appendChild(dateElem);
    historySection.appendChild(storeElem);
  }
}

function populateNotes(notes) {
  const notesSection = document.querySelector("[data-notes]")
  for (const note of notes) {
    const invoiceElem = document.createElement('p');
    const employeeElem = document.createElement('p');
    invoiceElem.innerText = note.invoiceNumber;
    employeeElem.innerText = `${note.employee.firstName} ${note.employee.lastName}`;
    notesSection.appendChild(invoiceElem);
    notesSection.appendChild(employeeElem);
  }
}

function populateEmployees(employees) {
  employeesArr = employees;
  const employeesSection = document.querySelector("[data-employees]");
  for (const employee of employees) {
    const firstNameElem = document.createElement('p');
    firstNameElem.setAttribute("data-employee-id", employee.employeeId);
    
    const lastNameElem = document.createElement("p");
    lastNameElem.setAttribute("data-employee-id", employee.employeeId);
    
    firstNameElem.innerText = employee.firstName;
    lastNameElem.innerText = employee.lastName;
    employeesSection.appendChild(firstNameElem);
    employeesSection.appendChild(lastNameElem);
  }
  populateEmployeeDetails(employees[0]);

  employeesSection.addEventListener('click', (e) => {
    const employeeId = e.target.getAttribute("data-employee-id")
    if (employeeId) {

      const selectedEmployee = employeesArr.find(e => e.employeeId === Number(employeeId));
      populateEmployeeDetails(selectedEmployee);
    }
  });
}

function populateEmployeeDetails(employee) {
  document.querySelector("input[name='first-name']").value = employee.firstName;
  document.querySelector("input[name='last-name']").value = employee.lastName;
  document.querySelector("input[name='title']").value = employee.title;
  document.querySelector("input[name='birth-date']").value = employee.birthDate;
  document.querySelector("input[name='position']").value = employee.position;
}

async function populateData() {
  const company = await getCompanyData();
  populateCompanyDetails(company);
  populateHistory(company.orders);
  populateNotes(company.notes);
  populateEmployees(company.employees);
}

document.addEventListener('DOMContentLoaded', populateData, false);