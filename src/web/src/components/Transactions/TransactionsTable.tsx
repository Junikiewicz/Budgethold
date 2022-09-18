import React, {FC, useEffect, useMemo, useState} from 'react';
import MaterialReactTable, { MRT_ColumnDef } from 'material-react-table';
import {
    Box,
    Button,
    ListItemIcon,
    MenuItem,
    Typography,
    TextField,
} from '@mui/material';
import type {
    ColumnFiltersState,
    PaginationState,
    SortingState,
} from '@tanstack/react-table';
import {
    QueryClient,
    QueryClientProvider,
    useQuery,
} from '@tanstack/react-query';
import axios from 'axios';

import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { AccountCircle, Send } from '@mui/icons-material';
import {date} from "yup";
import {filterFns} from "@tanstack/react-table";

export interface CategoryDto {
    name: string;
    id: number;
};

export interface WalletDto {
    name: string;
    id: number;
};

export interface Transaction  {
    id: number;
    name: string;
    wallet: WalletDto;
    category: CategoryDto;
    amount: number;
    date: Date;
};

const data: Transaction[] = [
    {
        id: 15,
        name: 'Lidl',
        wallet: {
            name: 'Moje',
            id: 13
        },
        category: {
            name: 'Zakupy',
            id: 13
        },
        amount: 50,
        date: new Date()
    },
    {
        id: 15,
        name: 'Rossmann',
        wallet: {
            name: 'Wspólne',
            id: 13
        },
        category: {
            name: 'Uroda',
            id: 13
        },
        amount: 123,
        date: new Date()
    }
];

const TransactionTable: FC = () => {
    const [columnFilters, setColumnFilters] = useState<ColumnFiltersState>([]);
    const [globalFilter, setGlobalFilter] = useState('');
    const [sorting, setSorting] = useState<SortingState>([]);
    const [pagination, setPagination] = useState<PaginationState>({
        pageIndex: 0,
        pageSize: 10,
    });

    const columns = useMemo<MRT_ColumnDef<Transaction>[]>(
        () => [
            {
                id: 'transaction', //id used to define `group` column
                header: 'Transactions',
                columns: [
                    {
                        accessorKey: 'name',
                        header: 'Name',
                        showColumnFilters: true,
                        size: 200,
                        Cell: ({ cell, row }) => (
                            <Box
                                sx={{
                                    display: 'flex',
                                    alignItems: 'center',
                                    gap: '1rem',
                                }}
                            >
                                <Typography>{cell.getValue<string>()}</Typography>
                            </Box>
                        ),
                    },
                    {
                        accessorFn: (row) => new Date(row.date),
                        id: 'date',
                        header: 'Operation Date',
                        enableClickToCopy: true,
                        filterFn: 'lessThanOrEqualTo',
                        sortingFn: 'datetime',
                        size: 200,
                        Cell: ({ cell }) => cell.getValue<Date>()?.toLocaleDateString(), //render Date as a string
                        Header: ({ column }) => <em>{column.columnDef.header}</em>, //custom header markup
                        //Custom Date Picker Filter from @mui/x-date-pickers
                        Filter: ({ column }) => (
                            <LocalizationProvider dateAdapter={AdapterDayjs}>
                                <DatePicker
                                    onChange={(newValue) => {
                                        column.setFilterValue(newValue);
                                    }}
                                    renderInput={(params) => (
                                        <TextField
                                            {...params}
                                            helperText={'Filter Mode: Lesss Than'}
                                            sx={{ minWidth: '120px' }}
                                            variant="standard"
                                        />
                                    )}
                                    value={column.getFilterValue()}
                                />
                            </LocalizationProvider>
                        ),
                    },
                    {
                        accessorKey: 'amount',
                        header: 'Amount',
                        showColumnFilters: true,
                        enableClickToCopy: true,
                        Cell: ({ cell, row }) => (
                            <Box
                                sx={{
                                    display: 'flex',
                                    alignItems: 'center',
                                    gap: '1rem',
                                }}
                            >
                                {cell.getValue<number>()?.toLocaleString?.('pl-PL', {
                                    style: 'currency',
                                    currency: 'PLN',
                                    minimumFractionDigits: 0,
                                    maximumFractionDigits: 0,
                                })}
                            </Box>
                        ),

                    },
                    {
                        accessorKey: 'category.name',
                        header: 'Category',
                        size: 200,
                    },
                    {
                        accessorKey: 'wallet.name',
                        header: 'Wallet',
                        size: 200,
                    },
                ],
            }
        ],
        [],
    );

    useEffect(() => {
        console.log("columnFilters");
        console.log(columnFilters);
        console.log("columns");
        console.log(columns);
        console.log("filters");
        columns[0].columns?.forEach(x => {
            console.log(x.filterFn.name);
        })
        console.log("sorting");
        console.log(sorting);
    }, [columnFilters, globalFilter, sorting, pagination])

    return (
        <MaterialReactTable
            columns={columns}
            data={data}
            enableColumnFilterModes
            enableColumnFilters
            manualFiltering
            manualPagination
            manualSorting
            enableColumnOrdering
            enableGrouping
            enablePinning
            enableRowSelection
            onColumnFiltersChange={setColumnFilters}
            onGlobalFilterChange={setGlobalFilter}
            onPaginationChange={setPagination}
            onSortingChange={setSorting}
            state={{
                columnFilters,
                globalFilter,
                pagination,
                sorting,
            }}
            initialState={{ showColumnFilters: true }}
            positionToolbarAlertBanner="bottom"
            renderTopToolbarCustomActions={({ table }) => {
                const handleDeactivate = () => {
                    table.getSelectedRowModel().flatRows.map((row) => {
                        alert('tu będzie usuwanie chyba ' + row.getValue('name'));
                    });
                };

                const handleActivate = () => {
                    table.getSelectedRowModel().flatRows.map((row) => {
                        alert('tu edycja ' + row.getValue('name'));
                    });
                };

                return (
                    <div style={{ display: 'flex', gap: '0.5rem' }}>
                        <Button
                            color="error"
                            disabled={table.getSelectedRowModel().flatRows.length === 0}
                            onClick={handleDeactivate}
                            variant="contained"
                        >
                            Usuń
                        </Button>
                        <Button
                            color="primary"
                            disabled={table.getSelectedRowModel().flatRows.length === 0}
                            onClick={handleActivate}
                            variant="contained"
                        >
                            Edytuj
                        </Button>
                    </div>
                );
            }}
        />
    );
};

export default TransactionTable;
