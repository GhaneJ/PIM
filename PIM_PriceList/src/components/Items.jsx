import { Button, ButtonGroup, Grid, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, withStyles } from '@mui/material';
import React, { useState, useEffect } from 'react';
import { connect } from 'react-redux';
import * as actions from '../actions/item';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import toast from 'react-hot-toast';
import ItemForm from './ItemForm';

const Items = ({ classes, ...props }) => {

    const [currentId, setCurrentId] = useState(0);

    useEffect(() => {
        props.fetchAllItems()
    }, [])

    const notifyDelete = () => toast("Deleted successfully.");

    const onDelete = itemName => {
        if (window.confirm('Are you sure you want to delete this record?'))
            props.deleteItems(itemName, notifyDelete())
    }

    return (
        <Paper elevation={3}>
            <Grid container>
                <Grid item xs={6}>
                    <ItemForm {...({ currentId, setCurrentId })} />
                </Grid>
                <Grid item xs={6}>
                    <TableContainer>
                        <Table>
                            <TableHead>
                                <TableRow>
                                    <TableCell>Item Name</TableCell>
                                    <TableCell>Price</TableCell>
                                    <TableCell></TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {
                                    props.itemList.map((record, index) => {
                                        return (
                                            <TableRow key={index}>
                                                <TableCell>{record.itemName}</TableCell>
                                                <TableCell>{record.itemRetailPrice}</TableCell>
                                                <TableCell>
                                                    <ButtonGroup variant="text">
                                                        <Button><EditIcon color="primary"
                                                            onClick={() => { setCurrentId(record.itemName) }} /></Button>
                                                        <Button><DeleteIcon color="secondary" onClick={() => onDelete(record.itemName)} /></Button>
                                                    </ButtonGroup>
                                                </TableCell>
                                            </TableRow>

                                        )
                                    })
                                }
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Grid>
            </Grid>
        </Paper>
    )
}

const mapStateToProps = state => {
    return {
        itemList: state.item.list
    }
}

const mapActionToProps = {
    fetchAllItems: actions.fetchAll,
    deleteItems: actions.Delete
}

export default connect(mapStateToProps, mapActionToProps)(Items);