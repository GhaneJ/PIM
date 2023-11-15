import { Button, FormControl, FormHelperText, Grid, InputLabel, MenuItem, Select, TextField } from '@mui/material';
import React, { useState, useEffect } from 'react';
import useForm from './useForm';
import { connect } from 'react-redux';
import * as actions from '../actions/item';
import toast from 'react-hot-toast';

const initialFieldValues = {
  itemName: '',
  itemRetailPrice: ''
}

const ItemForm = (props) => {

  // validate() for validation of form as a whole, validate({fullName: 'John'}) for validating a single record
  // Making the following entries manatory, They are not null in Db as well
  // Add fieldValues parameter for realtime field validation

  const validate = (fieldValues = values) => {
    let temp = {...errors}
    if ('itemName' in fieldValues)
      temp.itemName = fieldValues.itemName != "" ? "" : "This field is required."
    
      if ('itemRetailPrice' in fieldValues)
      temp.itemRetailPrice = fieldValues.itemRetailPrice > 0 ? "" : "Price must be over 0."
    setErrors({
      ...temp
    })
    // To check the validity of this form as a whole
    if (fieldValues == values)
      return Object.values(temp).every(x => x == "")  }


  const {
    values,
    setValues,
    errors,
    setErrors,
    handleInputChange,
    resetForm
  } = useForm(initialFieldValues, validate, props.setCurrentId)

  const notify = () => toast("submitted successfully.");

  const handleSubmit = e => {
    e.preventDefault()
    if (validate()) {
      const onSuccess = () => {resetForm()
       notify()
      }
      if (props.currentId == 0) //if nothing has Changed, insert the same data into Db.
      props.createItem(values, onSuccess)
      else
      //else update the data
      props.updateItem(props.currentId, values, onSuccess)
    }
    
  }  

  // Whenever currentId value changes, we use useEffect to populate the record
  // to be updated in form elements, so that user can change the values.
  
  useEffect(() => {
    if (props.currentId != 0) {
      setValues({
        ...props.itemList.find(x => x.itemName == props.currentId)
      })
      setErrors({})
    }
  }, [props.currentId])

  return (
    <form autoComplete="off" noValidate onSubmit={handleSubmit}>
      <Grid sx={{ m: 15, minWidth: 210 }} >
        <Grid item xs={6}>
          <TextField sx={{ m: .5, minWidth: 210 }}
            name="itemName"
            variant="outlined"
            label="Item Name"
            value={values.itemName}
            onChange={handleInputChange}
            {...(errors.itemName && { error: true, helperText: errors.itemName })}
          />         
        </Grid>
        <Grid item xs={6}>          
          <TextField sx={{ m: .5, minWidth: 210 }}
            name="itemRetailPrice"
            variant="outlined"
            label="Item Retail Price"
            value={values.itemRetailPrice}
            onChange={handleInputChange}
            {...(errors.itemRetailPrice && { error: true, helperText: errors.itemRetailPrice })}
          />          
          <Button sx={{ m: 1, minWidth: 110 }} variant="contained" color="primary" type="submit">
            Submit
          </Button>
          <Button sx={{ m: 1, minWidth: 110 }} variant="outlined" onClick={resetForm}>
            Reset
            </Button>
        </Grid>
      </Grid>
    </form>
  )
}

const mapStateToProps = state => ({  
    itemList:state.item.list
})

const mapActionToProps = {
  createItem: actions.create,
  updateItem: actions.update
}

export default connect(mapStateToProps, mapActionToProps)(ItemForm);